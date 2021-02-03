using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Legalpedia.Tags.Dtos
{
    [AutoMapFrom(typeof(Models.Tag))]
    public class TagDto:EntityDto
    {
        public string Text { get; set; }
    }
}
