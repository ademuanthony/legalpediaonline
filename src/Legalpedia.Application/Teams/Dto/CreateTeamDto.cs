using Abp.AutoMapper;
using Legalpedia.Models;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Legalpedia.Teams.Dto
{
    [AutoMapTo(typeof(Team))]
    public class CreateTeamDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Logo { get; set; }
    }
}