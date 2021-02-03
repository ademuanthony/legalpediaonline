using Abp.AutoMapper;

namespace Legalpedia.Tags.Dtos
{
    [AutoMapTo(typeof(Models.Tag))]
    public class CreateTagDto
    {
        public string Text { get; set; }
    }
}
