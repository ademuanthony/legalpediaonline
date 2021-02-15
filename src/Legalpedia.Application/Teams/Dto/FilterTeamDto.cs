using Abp.Application.Services.Dto;

namespace Legalpedia.Teams.Dto
{
    public class FilterTeamDto: PagedResultRequestDto
    {
        public string Name { get; set; }
    }
}