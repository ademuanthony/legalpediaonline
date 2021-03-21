using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Teams.Dto
{
    [AutoMapFrom(typeof(Team))]
    public class TeamDto:EntityDto<string>
    {
        public long CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}