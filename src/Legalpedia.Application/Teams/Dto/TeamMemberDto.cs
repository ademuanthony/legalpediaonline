using Abp.AutoMapper;
using Legalpedia.Models;
using Abp.Application.Services.Dto;

namespace Legalpedia.Teams.Dto
{
    [AutoMapTo(typeof(TeamMember))]
    [AutoMapFrom(typeof(TeamMember))]
    public class TeamMemberDto:EntityDto<string>
    {
        public string TeamId { get; set; }
        public long UserId { get; set; }
        public TeamRole Role { get; set; }
    }
}