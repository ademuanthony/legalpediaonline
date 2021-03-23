using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Teams.Dto;

namespace Legalpedia.Teams
{
    public interface ITeamsAppService : IAsyncCrudAppService<TeamDto, string,
        PagedResultRequestDto, CreateTeamDto, UpdateTeamDto>
    {
        Task<PagedResultDto<MyTeamOutput>> MyTeams(PagedResultRequestDto input);
        Task<PagedResultDto<TeamDto>> Filter(FilterTeamDto input);
        Task<string> TeamLogo(EntityDto<string> entityDto);
        Task<PagedResultDto<TeamMemberInfo>> GetTeamMembers(FetchTeamDto input);
        Task<TeamMemberInfo> AddTeamMember(CreateTeamMemberDto input);
        Task<bool> ChangeRole(ChangeRoleInput input);
        Task<TeamMemberInfo> RemoveTeamMember(UpdateTeamMemberDto input);

    }
}
