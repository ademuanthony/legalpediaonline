using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Legalpedia.Authorization.Roles;
using Legalpedia.Authorization.Users;
using Legalpedia.MultiTenancy;
using Abp.Localization;
using Legalpedia.Models;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Legalpedia.EntityFrameworkCore
{
    public class LegalpediaDbContext : AbpZeroDbContext<Tenant, Role, User, LegalpediaDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<AreaOfLaw> AreaOfLaws { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coram> Corams { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dictionary> Dictonaries { get; set; }
        public DbSet<ForeignLegalResource> ForeignLegalResources { get; set; }
        public DbSet<FormsPrecedence> FormsPrecedences { get; set; }
        public DbSet<HoldenAt> HoldenAts { get; set; }
        public DbSet<Index> Indices { get; set; }
        public DbSet<Judgement> Judgements { get; set; }
        public DbSet<JudgementPage> JudgementPages { get; set; }
        public DbSet<JudgementCoram> JudgementCorams { get; set; }
        public DbSet<JudgementCounsel> JudgementCounsels { get; set; }
        public DbSet<JudgementPartiesA> JudgementPartiesAs { get; set; }
        public DbSet<JudgementPartiesB> JudgementPartiesBs { get; set; }
        public DbSet<JudgementPrinciple> JudgementPrinciples { get; set; }
        public DbSet<JudgementsSummary> JudgementsSummaries { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<KeywordRanking> KeywordRankings { get; set; }
        public DbSet<LawOfFederation> LasLawOfFederations { get; set; }
        public DbSet<LawOfFedPart> LawOfFedParts { get; set; }
        public DbSet<LawOfFedSched> LawOfFedScheds { get; set; }
        public DbSet<LawOfFedSection> LawOfFedSections { get; set; }
        public DbSet<LegalUpdate> LegalUpdates { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Maxim> Maxims { get; set; }
        public DbSet<OneTimePassword> OneTimePasswords { get; set; }
        public DbSet<OtpLicense> OtpLicenses { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PartyAType> PartyATypes { get; set; }
        public DbSet<PartyBType> PartyBTypes { get; set; }
        public DbSet<Principle> Principles { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<ResultVote> ResultVotes { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<SbjMatterIndex> SbjMatterIndices { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<SumAreasOfLaw> SumAreasOfLaws { get; set; }
        public DbSet<SummaryRatio> SummaryRatios { get; set; }
        public DbSet<Synchronization> Synchronizations { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Update> Updates { get; set; }
        public DbSet<UpdateMeta> UpdateMetas { get; set; }


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

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(
                    entity,
                    validationContext,
                    validateAllProperties: true);
            }

            return base.SaveChanges();
        }
    }
}
