using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Legalpedia.Authorization;

namespace Legalpedia
{
    [DependsOn(
        typeof(LegalpediaCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class LegalpediaApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<LegalpediaAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(LegalpediaApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
