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

namespace Legalpedia.Teams
{
   // [AbpAuthorize(PermissionNames.TeamPage)]
    public class TeamsAppService : AsyncCrudAppService<Team,
        TeamDto, int,
        PagedResultRequestDto, CreateTeamDto, UpdateTeamDto>, ITeamsAppService
    {
        private readonly IRepository<TeamMember> _teamMembetRepository;
        private readonly UserManager _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        public TeamsAppService(IRepository<Team> repository,
            IRepository<TeamMember> teamMembetRepository, UserManager userManager,
            IHostingEnvironment hostingEnvironment) : base(repository)
        {
            _teamMembetRepository = teamMembetRepository;
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
            if (Repository.GetAll().Any(a => a.Uuid == input.Uuid))
            {
                throw new UserFriendlyException("Team already exists");
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
            return base.CreateAsync(input);
        }

        public TeamDto GetByUuid(EntityDto<string> input)
        {
            var team = Repository.FirstOrDefault(art => art.Uuid == input.Id);
            return ObjectMapper.Map<TeamDto>(team);
        }
        public async Task<PagedResultDto<TeamDto>> Filter(FilterTeamDto input)
        {
            if (string.IsNullOrEmpty(input.Name))
            {
                var result = await base.GetAllAsync(input);
                return result;
            }
            var TeamsQuery = Repository.GetAll().Where(a => a.Name.ToLower().Contains(input.Name.ToLower()));
            var Teams = TeamsQuery.Select(art => new TeamDto
            {
                Uuid = art.Uuid,
                Name = art.Name,
                Description = art.Description,
                Logo = art.Logo,
            }).OrderBy(art => art.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var totalCount = TeamsQuery.Count();
            return new PagedResultDto<TeamDto>(totalCount, Teams);
        }

        public async Task<PagedResultDto<UserDto>> GetTeamMembers(FetchTeamDto input)
        {
            var teamMembers = _teamMembetRepository.GetAllIncluding(itm => itm.User)
                .Where(a => a.TeamUuid == input.TeamUuid).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
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
            var team = _teamMembetRepository.GetAll().FirstOrDefault(a => a.UserId == input.UserId && a.TeamUuid == input.TeamUuid);
            if (team != null)
            {
                throw new UserFriendlyException((int)HttpStatusCode.BadRequest, $"The member is already in this team.");
            }
            if (Repository.GetAll().Any(a => a.Uuid == input.Uuid))
            {
                throw new UserFriendlyException("Team already exists");
            }
            var teamMember = await _teamMembetRepository.InsertAsync(ObjectMapper.Map<TeamMember>(input));
            await CurrentUnitOfWork.SaveChangesAsync();
            var dto = ObjectMapper.Map<TeamMemberDto>(teamMember);
            return dto;
        }

        public async Task<TeamMemberDto> RemoveTeamMember(UpdateTeamMemberDto input)
        {
            var team = _teamMembetRepository.GetAll().FirstOrDefault(a => a.UserId == input.UserId && a.TeamUuid == input.TeamUuid);
            if (team == null)
            {
                throw new UserFriendlyException((int)HttpStatusCode.NotFound, $"The team member not found.");
            }
            await _teamMembetRepository.DeleteAsync(team);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<TeamMemberDto>(team);
        }
        
    }
}
