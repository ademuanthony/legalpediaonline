using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Legalpedia.Models;
using Abp.Authorization;
using Legalpedia.Teams.Dto;
using System.Net;
using Legalpedia.Authorization.Users;
using System;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Legalpedia.Teams
{
   [AbpAuthorize]
    public class TeamsAppService : AsyncCrudAppService<Team,
        TeamDto, string,
        PagedResultRequestDto, CreateTeamDto, UpdateTeamDto>, ITeamsAppService
    {
        private readonly IRepository<TeamMember, string> _teamMemberRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<TeamLogo, string> _teamLogoRepository;
        public TeamsAppService(IRepository<Team, string> repository,
            IRepository<TeamMember, string> teamMemberRepository, IRepository<User, long> userRepository,
            IRepository<TeamLogo, string> teamLogoRepository) : base(repository)
        {
            _teamMemberRepository = teamMemberRepository;
            _userRepository = userRepository;
            _teamLogoRepository = teamLogoRepository;
        }

        
        public override async Task<TeamDto> CreateAsync(CreateTeamDto input)
        {
            try
            {
                if (AbpSession.UserId == null) throw new UserFriendlyException((int)HttpStatusCode.Unauthorized, "Please login first");
                if (input.Logo == null)
                {
                    throw new UserFriendlyException((int)HttpStatusCode.BadRequest, $"The team logo is required.");
                }
                if (await Repository.CountAsync(t=>t.Name.ToLower() == input.Name.ToLower()) > 0)
                {
                    throw new UserFriendlyException((int)HttpStatusCode.BadRequest, $"The team name '{input.Name}' already exists.");
                }

                var teamId = Guid.NewGuid().ToString();
                try
                {
                    // validate input
                    // var base64Data = Regex.Match(input.Logo, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                    // Convert.FromBase64String(base64Data);
                    await _teamLogoRepository.InsertAsync(new TeamLogo
                    {
                        Id = Guid.NewGuid().ToString(),
                        TeamId = teamId,
                        Base64 = input.Logo
                    });
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new UserFriendlyException((int)HttpStatusCode.InternalServerError, ex.Message);
                }

                var team = new Team
                {
                    Id = teamId,
                    Name = input.Name,
                    Description = input.Description,
                    CreationTime = DateTime.Now,
                    CreatorUserId = AbpSession.UserId.Value,
                };
                await Repository.InsertAsync(team);

                await _teamMemberRepository.InsertAsync(new TeamMember
                {
                    TeamId = team.Id,
                    UserId = AbpSession.UserId.Value,
                    Id = Guid.NewGuid().ToString(),
                    Role = TeamRole.Owner,
                });
                return ObjectMapper.Map<TeamDto>(team);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new UserFriendlyException(e.Message);
            }
        }

        public override async Task<TeamDto> UpdateAsync(UpdateTeamDto input)
        {
            if (input.Logo.IsNullOrEmpty()) return await base.UpdateAsync(input);

            try
            {
                // Convert.FromBase64String(input.Logo);
                var logo = await _teamLogoRepository.FirstOrDefaultAsync(tl => tl.TeamId == input.Id);
                if (logo == null)
                {
                    await _teamLogoRepository.InsertAsync(new TeamLogo
                    {
                        Id = Guid.NewGuid().ToString(),
                        TeamId = input.Id,
                        Base64 = input.Logo
                    });
                }
                else
                {
                    logo.Base64 = input.Logo;
                    await _teamLogoRepository.UpdateAsync(logo);
                }
                return await base.UpdateAsync(input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
                CreatorId = art.CreatorUserId.Value,
                Name = art.Name,
                Description = art.Description,
            }).OrderBy(art => art.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var totalCount = teamsQuery.Count();
            return new PagedResultDto<TeamDto>(totalCount, teams);
        }

        public async Task<string> TeamLogo(EntityDto<string> entityDto)
        {
            var logo = await _teamLogoRepository.FirstOrDefaultAsync(tl => tl.TeamId == entityDto.Id);
            if (logo == null)
            {
                throw new UserFriendlyException("Logo not found");
            }

            return logo.Base64;
        }
        
        public async Task<PagedResultDto<MyTeamOutput>> MyTeams(PagedResultRequestDto input)
        {
            try
            {
                var query = from t in Repository.GetAll()
                    join tm in _teamMemberRepository.GetAll() on t.Id equals tm.TeamId
                    where tm.UserId == AbpSession.UserId.Value
                    select new {Team = tm.Team, Role = tm.Role};

                var totalCount = await query.CountAsync();
                var teams = query.OrderBy(t => t.Team.Id)
                    .Skip(input.SkipCount).Take(input.MaxResultCount)
                    .Select(art => new MyTeamOutput
                    {
                        Id = art.Team.Id,
                        Name = art.Team.Name,
                        Description = art.Team.Description,
                        CreatorId = art.Team.CreatorUserId.Value,
                        Role = art.Role
                    }).ToList();
                return new PagedResultDto<MyTeamOutput>(totalCount, teams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedResultDto<TeamMemberInfo>> GetTeamMembers(FetchTeamDto input)
        {
            var query = (from tm in _teamMemberRepository.GetAll()
                join u in _userRepository.GetAll() on tm.UserId equals u.Id
                orderby tm.Id
                where tm.TeamId == input.TeamId
                select new TeamMemberInfo
                {
                    TeamId =  tm.TeamId,
                    Role = tm.Role, 
                    Username = u.UserName, 
                    DisplayName = u.FullName, 
                    UserId = u.Id
                });

            var totalCount = await query.CountAsync();
            var members = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            
            return new PagedResultDto<TeamMemberInfo>(totalCount, members);
        }

        public async Task<TeamMemberInfo> AddTeamMember(CreateTeamMemberDto input)
        {
            try
            {
                var team = await Repository.FirstOrDefaultAsync(t => t.Id == input.TeamId);
                if (team.CreatorUserId != AbpSession.UserId.Value)
                {
                    var currentMember = await _teamMemberRepository.FirstOrDefaultAsync(tm => tm.UserId == AbpSession.UserId.Value
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

                var tm = ObjectMapper.Map<TeamMember>(input);
                tm.Id = Guid.NewGuid().ToString();
                await _teamMemberRepository.InsertAndGetIdAsync(tm);
                var userInfo = await _userRepository.FirstOrDefaultAsync(u => u.Id == input.UserId);
                return new TeamMemberInfo
                {
                    TeamId = input.TeamId,
                    Role = tm.Role,
                    Username = userInfo.UserName,
                    DisplayName = userInfo.FullName,
                    UserId = tm.UserId
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> ChangeRole(ChangeRoleInput input)
        {
            var teamMember = await _teamMemberRepository.FirstOrDefaultAsync(tm => tm.Id == input.TeamMemberId);
            if ((await Repository.CountAsync(t => t.Id == teamMember.TeamId 
                                                  && t.CreatorUserId == AbpSession.UserId.Value)) == 0)
            {
                throw new UserFriendlyException("You are not the creator of this team");
            }
            teamMember.Role = input.Role;
            await _teamMemberRepository.UpdateAsync(teamMember);
            return true;
        }

        public async Task<TeamMemberInfo> RemoveTeamMember(UpdateTeamMemberDto input)
        {
            var team = await Repository.FirstOrDefaultAsync(t => t.Id == input.TeamId);
            if (AbpSession.UserId == null) throw new UserFriendlyException("Please login");
            if (team.CreatorUserId != AbpSession.UserId.Value)
            {
                var currentMember = await _teamMemberRepository.FirstOrDefaultAsync(tm => tm.UserId == AbpSession.UserId.Value
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
            var userInfo = await _userRepository.FirstOrDefaultAsync(u => u.Id == input.UserId);
            return new TeamMemberInfo
            {
                TeamId = input.TeamId,
                Role = oldRecord.Role,
                Username = userInfo.UserName,
                DisplayName = userInfo.FullName,
                UserId = oldRecord.UserId
            };
        }
        
    }
}
