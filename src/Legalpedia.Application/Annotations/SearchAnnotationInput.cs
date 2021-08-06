using Abp.Application.Services.Dto;

namespace Legalpedia.Annotations
{
    public class SearchAnnotationInput: PagedResultRequestDto
    {
        public string Term { get; set; }
    }
}