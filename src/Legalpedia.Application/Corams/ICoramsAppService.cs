using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Corams.Dto;

namespace Legalpedia.Corams
{
    public interface ICoramsAppService : IAsyncCrudAppService<CoramDto, int,
        PagedResultRequestDto, CreateCoramDto, UpdateCoramDto>
    {
    }
}
