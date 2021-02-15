using Abp.AutoMapper;
using Legalpedia.Models;
using System;
namespace Legalpedia.Teams.Dto
{
    [AutoMapTo(typeof(TeamMember))]
    [AutoMapFrom(typeof(TeamMember))]
    public class TeamMemberDto
    {
        public string Uuid { get; set; }
        public string TeamUuid { get; set; }
        public long UserId { get; set; }

    }
}