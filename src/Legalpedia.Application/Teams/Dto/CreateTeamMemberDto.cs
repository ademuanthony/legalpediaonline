using Abp.AutoMapper;
using Legalpedia.Models;
using System.ComponentModel.DataAnnotations;

namespace Legalpedia.Teams.Dto
{
    [AutoMapTo(typeof(TeamMember))]
    public class CreateTeamMemberDto
    {
        [Required]
        public string TeamId { get; set; }
        [Required]
        public long UserId { get; set; }
        
        public TeamRole Role { get; set; }
    }
}