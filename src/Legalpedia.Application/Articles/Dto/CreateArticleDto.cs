using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Articles.Dto
{
    [AutoMapTo(typeof(Article))]
    public class CreateArticleDto
    {
        public string Uuid { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? VersionNo { get; set; }
    }
}