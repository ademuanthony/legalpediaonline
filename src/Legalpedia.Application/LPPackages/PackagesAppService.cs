using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Legalpedia.Authorization.Roles;
using Legalpedia.Models;
using Legalpedia.Packages.Dto;
using Legalpedia.Roles;
using System.Threading.Tasks;

namespace Legalpedia.Packages
{
    public class PackagesAppService : AsyncCrudAppService<Package,
        PackageDto, int,
        PagedResultRequestDto, CreatePackageDto, UpdatePackageDto>, IPackagesAppService
    {
        IRoleAppService _roleService;
        IRepository<Role> _roleRepository;

        public PackagesAppService(IRepository<Package> repository, IRoleAppService roleService, IRepository<Role> roleRepository) : base(repository)
        {
            _roleService = roleService;
            _roleRepository = roleRepository;
        }

        public PackageDto GetByKey(string key)
        {
            var package = Repository.FirstOrDefault(p => p.Permalink.ToLower() == key.ToLower());
            return package == null ? null : package.MapTo<PackageDto>();
        }

        public override async Task<PackageDto> CreateAsync(CreatePackageDto input)
        {
            try
            {
                if (await _roleRepository.FirstOrDefaultAsync(r => r.Name == input.Name) == null)
                    await _roleService.CreateAsync(new Roles.Dto.CreateRoleDto
                    {
                        Description = input.Description,
                        DisplayName = input.Name,
                        Name = input.Name,
                        NormalizedName = input.Name,
                    });

                var pkg = new Package
                {
                    CreationTime = DateTime.Now,
                    LastModificationTime = DateTime.Now,
                    CreatorUserId = AbpSession.UserId.Value,
                    LastModifierUserId = AbpSession.UserId.Value,
                    Days = input.Days,
                    Description = input.Description,
                    Features = input.Features,
                    Name = input.Name,
                    Permalink = input.Permalink,
                    Platforms = input.Platforms,
                    Price = input.Price,
                    Id = Repository.GetAll().Max(p => p.Id) + 1
                };
                await Repository.InsertAsync(pkg);
                
                return ObjectMapper.Map<PackageDto>(pkg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public override async Task<PackageDto> UpdateAsync(UpdatePackageDto input)
        {
            try
            {
                var rec = await Repository.FirstOrDefaultAsync(p => p.Id == input.Id);
                input.CreationTime = rec.CreationTime;
                input.CreatorUserId = rec.CreatorUserId;
                input.LastModificationTime = DateTime.Now;
                input.LastModifierUserId = AbpSession.UserId.Value;
            
                return await base.UpdateAsync(input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
