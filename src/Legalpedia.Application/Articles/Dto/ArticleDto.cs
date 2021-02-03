using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Articles.Dto
{
    [AutoMapFrom(typeof(Article))]
    public class ArticleDto:EntityDto
    {
        public string Uuid { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? VersionNo { get; set; }
    }
}