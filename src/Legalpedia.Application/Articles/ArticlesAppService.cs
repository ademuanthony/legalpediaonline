using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Legalpedia.Models;
using Legalpedia.Articles.Dto;

namespace Legalpedia.Articles
{
    public class ArticlesAppService : AsyncCrudAppService<Article,
        ArticleDto, int,
        PagedResultRequestDto, CreateArticleDto, UpdateArticleDto>, IArticlesAppService
    {
        public ArticlesAppService(IRepository<Article> repository) : base(repository)
        {

        }

        public async Task<PagedResultDto<ArticleDto>> Filter(FilterArticlesDto input)
        {
            if (string.IsNullOrEmpty(input.Title))
            {
                var result = await base.GetAllAsync(input);
                return result;
            }
            var articlesQuery = Repository.GetAll().Where(a => a.Title.ToLower().Contains(input.Title.ToLower()));
            var articles = articlesQuery.Select(art => new ArticleDto
            {
                Id = art.Id,
                Uuid = art.Uuid,
                Title = art.Title
            }).OrderBy(art => art.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var totalCount = articlesQuery.Count();
            return new PagedResultDto<ArticleDto>(totalCount, articles);
        }

        public ArticleDto GetByUuid(EntityDto<string> input)
        {
            var article = Repository.FirstOrDefault(art => art.Uuid == input.Id);
            return article.MapTo<ArticleDto>();
        }

        public override Task<ArticleDto> CreateAsync(CreateArticleDto input)
        {
            if (Repository.GetAll().Any(art => art.Uuid == input.Uuid))
            {
                throw new UserFriendlyException("Article already exists");
            }
            return base.CreateAsync(input);
        }
    }
}
