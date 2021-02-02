using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Legalpedia.Configuration;

namespace Legalpedia.Web.Host.Startup
{
    [DependsOn(
       typeof(LegalpediaWebCoreModule))]
    public class LegalpediaWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public LegalpediaWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LegalpediaWebHostModule).GetAssembly());
        }
    }
}
