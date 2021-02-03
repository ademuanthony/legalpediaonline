using Abp.EntityFrameworkCore;
using Legalpedia.Models;

namespace Legalpedia.EntityFrameworkCore.Repositories
{
    public class LawSectionRepository : LegalpediaRepositoryBase<LawOfFedSection>, ILawSectionRepository
    {
        public LawSectionRepository(IDbContextProvider<LegalpediaDbContext> provider) : base(provider)
        {

        }
    }
}