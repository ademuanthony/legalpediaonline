using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Legalpedia.Teams.Dto
{
    public class FetchTeamDto : PagedResultRequestDto
    {
        [Required]
        public string TeamUuid { get; set; }
    }
}