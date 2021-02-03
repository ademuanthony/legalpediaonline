using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Legalpedia.Tags.Dtos;

namespace Legalpedia.Tags
{
    public class TagsAppService : AsyncCrudAppService<Models.Tag,
        TagDto, int,
        PagedResultRequestDto, CreateTagDto, UpdateTagDto>, ITagsAppService
    {
        public TagsAppService(IRepository<Models.Tag> repository) : base(repository)
        {

        }
    }
}
