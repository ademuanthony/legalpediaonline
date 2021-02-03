using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.ForeignLegalResources.Dtos;

namespace Legalpedia.ForeignLegalResources
{
    public interface IForeignLegalResourcesAppService : IAsyncCrudAppService<CreateForeignLegalResourceDto, int,
        PagedResultRequestDto, CreateForeignLegalResourceDto, UpdateForeignLegalResourceDto>
    {
    }
}
