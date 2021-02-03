using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Articles.Dto;

namespace Legalpedia.Articles
{
    public interface IArticlesAppService : IAsyncCrudAppService<ArticleDto, int,
        PagedResultRequestDto, CreateArticleDto, UpdateArticleDto>
    {
        Task<PagedResultDto<ArticleDto>> Filter(FilterArticlesDto input);
        ArticleDto GetByUuid(EntityDto<string> input);
    }
}
