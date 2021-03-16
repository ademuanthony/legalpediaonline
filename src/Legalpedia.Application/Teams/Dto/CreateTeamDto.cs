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
        public long CreatorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Logo { get; set; }
    }
}