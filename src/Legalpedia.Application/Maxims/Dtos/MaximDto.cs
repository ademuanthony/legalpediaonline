using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Maxims.Dtos
{
    [AutoMapFrom(typeof(Maxim), typeof(Dictionary))]
    public class MaximDto: EntityDto
    {
        public string Uuid { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? VersionNo { get; set; }
    }
}
