using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Principles.Dto
{
    [AutoMapTo(typeof(Principle))]
    public class UpdatePrincipleDto:EntityDto
    {
        public string Principle { get; set; }

        public int SbjMatterIdxId { get; set; }
    }
}
