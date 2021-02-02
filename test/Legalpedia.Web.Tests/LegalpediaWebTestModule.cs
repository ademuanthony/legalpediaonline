using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Legalpedia.EntityFrameworkCore;
using Legalpedia.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Legalpedia.Web.Tests
{
    [DependsOn(
        typeof(LegalpediaWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class LegalpediaWebTestModule : AbpModule
    {
        public LegalpediaWebTestModule(LegalpediaEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LegalpediaWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(LegalpediaWebMvcModule).Assembly);
        }
    }
}