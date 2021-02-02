using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Legalpedia.EntityFrameworkCore
{
    public static class LegalpediaDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<LegalpediaDbContext> builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<LegalpediaDbContext> builder, DbConnection connection)
        {
            builder.UseNpgsql(connection);
        }
    }
}
