using Abp.AutoMapper;
using Legalpedia.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Legalpedia.Teams.Dto
{
    [AutoMapTo(typeof(TeamMember))]
    public class UpdateTeamMemberDto
    {
        [Required]
        public string TeamUuid { get; set; }
        [Required]
        public long UserId { get; set; }
    }
}