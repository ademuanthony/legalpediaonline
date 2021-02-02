using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Legalpedia.Authorization.Roles;
using Legalpedia.Authorization.Users;
using Legalpedia.MultiTenancy;
using Abp.Localization;

namespace Legalpedia.EntityFrameworkCore
{
    public class LegalpediaDbContext : AbpZeroDbContext<Tenant, Role, User, LegalpediaDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public LegalpediaDbContext(DbContextOptions<LegalpediaDbContext> options)
            : base(options)
        {
        }

        // add these lines to override max length of property
        // we should set max length smaller than the PostgreSQL allowed size (10485760)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationLanguageText>()
                .Property(p => p.Value)
                .HasMaxLength(100); // any integer that is smaller than 10485760
        }
    }
}
