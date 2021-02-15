using Abp.AutoMapper;
using Legalpedia.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Legalpedia.Teams.Dto
{
    [AutoMapTo(typeof(Team))]
    public class CreateTeamDto
    {
        public CreateTeamDto()
        {
            Uuid = Guid.NewGuid().ToString();
        }
        public string Uuid { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        //public IFormFile TeamLogo { get; set; }
        public string Logo { get; set; }
    }
}