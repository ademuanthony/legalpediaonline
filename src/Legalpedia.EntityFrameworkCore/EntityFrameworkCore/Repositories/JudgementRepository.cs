using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Legalpedia.Models;
using Legalpedia.Dto;
using Abp.EntityFrameworkCore;

namespace Legalpedia.EntityFrameworkCore.Repositories
{
    public interface IJudgementRepository:IRepository<Judgement, string>
    {
       ICollection<Judgement> GetForUpdate(DateTime lastUpdate, int offset, int limit);
        PagedResultDto<JudgementListItem> GetOrphanJudgements(GetJudgementRequest input);
    }

    public class JudgementRepository:LegalpediaRepositoryBase<Judgement, string>, IJudgementRepository
    {
        public JudgementRepository(IDbContextProvider<LegalpediaDbContext> provider) : base(provider)
        {

        }

        public ICollection<Judgement> GetForUpdate(DateTime lastUpdate, int offset, int limit)
        {
            return GetAll().OrderByDescending(s => s.Id).Skip(offset).Take(limit).ToList();
        }

        public PagedResultDto<JudgementListItem> GetOrphanJudgements(GetJudgementRequest input)
        {
            var query = from j in Context.Judgements
                where !(from s in Context.JudgementsSummaries select s.Id).Contains(j.Id) select j;
            
            var count = query.Count();
            var record = query.OrderBy(r => r.Id).Skip(input.SkipCount).Take(input.MaxResultCount).Select(j =>
                new JudgementListItem
                {
                    CaseTitle = j.Body.Substring(0, 300),
                    SuitNo = j.Id,
                    JudgementDate = j.CreatedAt
                }).ToList();
            return new PagedResultDto<JudgementListItem>(count, record);
        }
    }
}
