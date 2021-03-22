using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Legalpedia.Teams.Dto
{
    public class FetchTeamDto : PagedResultRequestDto
    {
        [Required]
        public string TeamId { get; set; }
    }
}