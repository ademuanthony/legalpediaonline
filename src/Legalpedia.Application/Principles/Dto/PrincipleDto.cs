using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Principles.Dto
{
    [AutoMapFrom(typeof(Principle))]
    public class PrincipleDto:EntityDto
    {
        public string Name { get; set; }

        public int SbjMatterIdxId { get; set; }
    }
}
