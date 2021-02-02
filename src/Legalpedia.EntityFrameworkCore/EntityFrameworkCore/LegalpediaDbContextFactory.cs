using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Legalpedia.Configuration;
using Legalpedia.Web;

namespace Legalpedia.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class LegalpediaDbContextFactory : IDesignTimeDbContextFactory<LegalpediaDbContext>
    {
        public LegalpediaDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LegalpediaDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            LegalpediaDbContextConfigurer.Configure(builder, configuration.GetConnectionString(LegalpediaConsts.ConnectionStringName));

            return new LegalpediaDbContext(builder.Options);
        }
    }
}
