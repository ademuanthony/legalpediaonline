using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Packages.Dto;

namespace Legalpedia.Packages
{
    public interface IPackagesAppService : IAsyncCrudAppService<PackageDto, int,
        PagedResultRequestDto, CreatePackageDto, UpdatePackageDto>
    {
        PackageDto GetByKey(string key);
    }
}
