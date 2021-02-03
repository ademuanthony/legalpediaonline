using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Legalpedia.Models;
using Abp.EntityFrameworkCore;

namespace Legalpedia.EntityFrameworkCore.Repositories
{
    public interface ISummaryRepository:IRepository<JudgementsSummary, string>
    {
        ICollection<JudgementsSummary> GetForUpdate(DateTime lastUpdatedAt, int offset, int limit);
    }

    public class SummaryRepository: LegalpediaRepositoryBase<JudgementsSummary, string>, ISummaryRepository
    {
        public SummaryRepository(IDbContextProvider<LegalpediaDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public ICollection<JudgementsSummary> GetForUpdate(DateTime lastUpdatedAt, int offset, int limit)
        {
            return GetAll()
                .Where(s=>s.CreatedAt >= lastUpdatedAt || s.UpdatedAt >= lastUpdatedAt)
                .OrderBy(s=>s.CreatedAt).Skip(offset).Take(limit).ToList();
        }
    }
}
