using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Teams.Dto;
using Legalpedia.Users.Dto;

namespace Legalpedia.Teams
{
    public interface ITeamsAppService : IAsyncCrudAppService<TeamDto, string,
        PagedResultRequestDto, CreateTeamDto, UpdateTeamDto>
    {
        Task<PagedResultDto<TeamDto>> MyTeams(PagedResultRequestDto input);
        Task<PagedResultDto<TeamDto>> Filter(FilterTeamDto input);
        Task<PagedResultDto<UserDto>> GetTeamMembers(FetchTeamDto input);
        Task<TeamMemberDto> AddTeamMember(CreateTeamMemberDto input);
        Task ChangeRole(ChangeRoleInput input);
        Task<TeamMemberDto> RemoveTeamMember(UpdateTeamMemberDto input);

    }
}
