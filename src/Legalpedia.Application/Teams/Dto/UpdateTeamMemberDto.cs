using Abp.AutoMapper;
using Legalpedia.Models;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Legalpedia.Teams.Dto
{
    [AutoMapTo(typeof(TeamMember))]
    public class UpdateTeamMemberDto:EntityDto<string>
    {
        [Required]
        public string TeamId { get; set; }
        [Required]
        public long UserId { get; set; }
        
        public TeamRole Role { get; set; }
    }
}