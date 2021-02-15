using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Teams.Dto;
using Legalpedia.Users.Dto;

namespace Legalpedia.Teams
{
    public interface ITeamsAppService : IAsyncCrudAppService<TeamDto, int,
        PagedResultRequestDto, CreateTeamDto, UpdateTeamDto>
    {
        Task<PagedResultDto<TeamDto>> Filter(FilterTeamDto input);
        TeamDto GetByUuid(EntityDto<string> input);
        Task<PagedResultDto<UserDto>> GetTeamMembers(FetchTeamDto input);
        Task<TeamMemberDto> AddTeamMember(CreateTeamMemberDto input);
        Task<TeamMemberDto> RemoveTeamMember(UpdateTeamMemberDto input);

    }
}
