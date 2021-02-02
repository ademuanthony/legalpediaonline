using Abp.Application.Services;
using Legalpedia.MultiTenancy.Dto;

namespace Legalpedia.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

