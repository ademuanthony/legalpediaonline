using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.ForeignLegalResources.Dtos
{
    [AutoMapFrom(typeof(ForeignLegalResource))]
    [AutoMapTo(typeof(ForeignLegalResource))]
    public class CreateForeignLegalResourceDto: EntityDto
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}