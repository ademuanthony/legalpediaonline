using Abp.AutoMapper;
using Legalpedia.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Legalpedia.Teams.Dto
{
    [AutoMapTo(typeof(TeamMember))]
    public class CreateTeamMemberDto
    {
        public CreateTeamMemberDto()
        {
            Uuid = Guid.NewGuid().ToString();
        }
        public string Uuid { get; set; }
        [Required]
        public string TeamUuid { get; set; }
        [Required]
        public long UserId { get; set; }
    }
}