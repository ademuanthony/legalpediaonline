using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Tags.Dtos;

namespace Legalpedia.Tags
{
    public interface ITagsAppService : IAsyncCrudAppService<TagDto, int,
        PagedResultRequestDto, CreateTagDto, UpdateTagDto>
    {
    }
}
