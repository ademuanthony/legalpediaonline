using Abp.Application.Services.Dto;

namespace Legalpedia.Articles.Dto
{
    public class FilterArticlesDto: PagedResultRequestDto
    {
        public string Title { get; set; }
    }
}