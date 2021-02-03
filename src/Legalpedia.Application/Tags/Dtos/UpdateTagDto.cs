using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Legalpedia.Tags.Dtos
{
    [AutoMapTo(typeof(Models.Tag))]
    public class UpdateTagDto:EntityDto
    {
        public string Text { get; set; }
    }
}
