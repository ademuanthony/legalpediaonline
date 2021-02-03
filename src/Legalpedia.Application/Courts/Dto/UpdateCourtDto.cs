using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Courts.Dto
{
    [AutoMapTo(typeof(Court))]
    public class UpdateCourtDto:EntityDto
    {
        public string Name { get; set; }
    }
}
