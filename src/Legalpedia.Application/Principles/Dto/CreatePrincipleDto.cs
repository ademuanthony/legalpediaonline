using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Principles.Dto
{
    [AutoMapTo(typeof(Principle))]
    public class CreatePrincipleDto
    {
        public string Name { get; set; }

        public int SbjMatterIdxId { get; set; }
    }
}
