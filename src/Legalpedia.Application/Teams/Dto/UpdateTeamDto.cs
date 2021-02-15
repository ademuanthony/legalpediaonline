using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Teams.Dto
{
    [AutoMapTo(typeof(Team))]
    public class UpdateTeamDto:EntityDto
    {
        public string Uuid { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
    }
}