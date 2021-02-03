using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Legalpedia.Categories.Dto;
using Legalpedia.Models;

namespace Legalpedia.Categories
{
    public class CategoriesAppService : AsyncCrudAppService<Category,
        CategoryDto, int,
        PagedResultRequestDto, CreateCategoryDto, UpdateCategoryDto>, ICategoriesAppService
    {
        public CategoriesAppService(IRepository<Category> repository) : base(repository)
        {

        }
    }
}
