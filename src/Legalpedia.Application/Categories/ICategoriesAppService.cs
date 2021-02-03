using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Categories.Dto;

namespace Legalpedia.Categories
{
    public interface ICategoriesAppService : IAsyncCrudAppService<CategoryDto, int,
        PagedResultRequestDto, CreateCategoryDto, UpdateCategoryDto>
    {
    }
}
