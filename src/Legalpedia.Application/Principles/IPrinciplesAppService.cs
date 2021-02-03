using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Principles.Dto;

namespace Legalpedia.Principles
{
    public interface IPrinciplesAppService : IAsyncCrudAppService<PrincipleDto, int,
        PagedResultRequestDto, CreatePrincipleDto, UpdatePrincipleDto>
    {
    }
}
