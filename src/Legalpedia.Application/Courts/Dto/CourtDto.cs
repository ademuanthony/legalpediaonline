using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Courts.Dto
{
    [AutoMapFrom(typeof(Court))]
    public class CourtDto:EntityDto
    {
        public string Name { get; set; }
    }
}
