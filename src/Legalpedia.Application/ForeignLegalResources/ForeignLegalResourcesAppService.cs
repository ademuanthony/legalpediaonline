using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Legalpedia.ForeignLegalResources.Dtos;
using Legalpedia.Models;

namespace Legalpedia.ForeignLegalResources
{
    public class ForeignLegalResourcesAppService : AsyncCrudAppService<ForeignLegalResource,
        CreateForeignLegalResourceDto, int,
        PagedResultRequestDto, CreateForeignLegalResourceDto, UpdateForeignLegalResourceDto>, IForeignLegalResourcesAppService
    {
        public ForeignLegalResourcesAppService(IRepository<ForeignLegalResource> repository) : base(repository)
        {
            
        }
    }
}
