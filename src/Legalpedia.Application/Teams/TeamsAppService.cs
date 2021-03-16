using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Legalpedia.Authorization;
using Abp.UI;
using Legalpedia.Models;
using Abp.Authorization;
using Legalpedia.Teams.Dto;
using System.Net;
using Legalpedia.Users.Dto;
using Legalpedia.Authorization.Users;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using Abp.Threading.Extensions;

namespace Legalpedia.Teams
{
   [AbpAuthorize]
    public class TeamsAppService : AsyncCrudAppService<Team,
        TeamDto, string,
        PagedResultRequestDto, CreateTeamDto, UpdateTeamDto>, ITeamsAppService
    {
        private readonly IRepository<TeamMember> _teamMemberRepository;
        private readonly UserManager _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public TeamsAppService(IRepository<Team, string> repository,
            IRepository<TeamMember> teamMemberRepository, UserManager userManager,
            IWebHostEnvironment hostingEnvironment) : base(repository)
        {
            _teamMemberRepository = teamMemberRepository;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public override Task<TeamDto> CreateAsync(CreateTeamDto input)
        {
            if (input.Logo == null)
            {
                throw new UserFriendlyException((int)HttpStatusCode.BadRequest, $"The team logo is required.");
            }

            var team = Repository.GetAll().FirstOrDefault(a => a.Name.ToLower().Contains(input.Name.ToLower()));
            if (team != null)
            {
                throw new UserFriendlyException((int)HttpStatusCode.BadRequest, $"The team name '{input.Name}' already exists.");
            }
            var uniqueFileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), ".png");
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, LegalpediaConsts.TeamsLogoFolder);
            if(!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            var filePath = Path.Combine(uploads, uniqueFileName);
            /* using (var stream = new FileStream(filePath, FileMode.Create))
             {
                 input.TeamLogo.CopyTo(stream);
             }*/
            try
            {
                byte[] bytes = Convert.FromBase64String(input.Logo);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    using (Image image = Image.FromStream(ms))
                    {
                        image.Save(filePath, ImageFormat.Png);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new UserFriendlyException((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            input.Logo = uniqueFileName;
            input.CreatorId = AbpSession.UserId.Value;
            return base.CreateAsync(input);
        }

        public async Task<PagedResultDto<TeamDto>> Filter(FilterTeamDto input)
        {
            if (string.IsNullOrEmpty(input.Name))
            {
                var result = await base.GetAllAsync(input);
                return result;
            }
            var teamsQuery = Repository.GetAll().Where(a => a.Name.ToLower().Contains(input.Name.ToLower()));
            var teams = teamsQuery.Select(art => new TeamDto
            {
                CreatorId = art.CreatorId,
                Name = art.Name,
                Description = art.Description,
                Logo = art.Logo,
            }).OrderBy(art => art.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var totalCount = teamsQuery.Count();
            return new PagedResultDto<TeamDto>(totalCount, teams);
        }

        public async Task<PagedResultDto<TeamDto>> MyTeams(PagedResultRequestDto input)
        {
            var query = from t in Repository.GetAll()
                join tm in _teamMemberRepository.GetAll() on t.Id equals tm.TeamId
                where tm.UserId == AbpSession.UserId.Value
                select t;

            var totalCount = query.Count();
            var teams = query.OrderBy(t => t.Id)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .Select(art =>new TeamDto
            {
                Name = art.Name,
                Description = art.Description,
                Logo = art.Logo,
                CreatorId = art.CreatorId
            }).ToList();
            return new PagedResultDto<TeamDto>(totalCount, teams);
        }

        public async Task<PagedResultDto<UserDto>> GetTeamMembers(FetchTeamDto input)
        {
            var teamMembers = _teamMemberRepository.GetAllIncluding(itm => itm.User)
                .Where(a => a.TeamId == input.TeamUuid).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var users = new List<UserDto>();
            foreach (var item in teamMembers)
            {
                users.Add(ObjectMapper.Map<UserDto>(item.User));
            }
            var totalCount = teamMembers.Count();
            return new PagedResultDto<UserDto>(totalCount, users);
        }

        public async Task<TeamMemberDto> AddTeamMember(CreateTeamMemberDto input)
        {
            var team = Repository.FirstOrDefault(t => t.Id == input.TeamId);
            if (team.CreatorId != AbpSession.UserId.Value)
            {
                var currentMember = _teamMemberRepository.FirstOrDefault(tm => tm.UserId == AbpSession.UserId.Value
                                                                               && tm.TeamId == input.TeamId && tm.Role == TeamRole.Admin);
                if (currentMember == null)
                {
                    throw new UserFriendlyException(
                        "Access Denied; you are neither the owner nor an admin of this team");
                }
            }
            var existingRecord = _teamMemberRepository.GetAll().FirstOrDefault(a => a.UserId == input.UserId && a.TeamId == input.TeamId);
            if (existingRecord != null)
            {
                throw new UserFriendlyException((int)HttpStatusCode.BadRequest, $"The member is already in this team.");
            }
            var teamMember = await _teamMemberRepository.InsertAsync(ObjectMapper.Map<TeamMember>(input));
            await CurrentUnitOfWork.SaveChangesAsync();
            var dto = ObjectMapper.Map<TeamMemberDto>(teamMember);
            return dto;
        }

        public async Task<TeamMemberDto> RemoveTeamMember(UpdateTeamMemberDto input)
        {
            var team = Repository.FirstOrDefault(t => t.Id == input.TeamId);
            if (team.CreatorId != AbpSession.UserId.Value)
            {
                var currentMember = _teamMemberRepository.FirstOrDefault(tm => tm.UserId == AbpSession.UserId.Value
                                                                               && tm.TeamId == input.TeamId && tm.Role == TeamRole.Admin);
                if (currentMember == null)
                {
                    throw new UserFriendlyException(
                        "Access Denied; you are neither the owner nor an admin of this team");
                }
            }
            var oldRecord = _teamMemberRepository.GetAll().FirstOrDefault(a => a.UserId == input.UserId && a.TeamId == input.TeamId);
            if (oldRecord == null)
            {
                throw new UserFriendlyException((int)HttpStatusCode.NotFound, $"The team member not found.");
            }
            await _teamMemberRepository.DeleteAsync(oldRecord);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<TeamMemberDto>(oldRecord);
        }
        
    }
}
