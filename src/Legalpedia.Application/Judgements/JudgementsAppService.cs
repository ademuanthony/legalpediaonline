using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.UI;
using Legalpedia.EntityFrameworkCore.Repositories;
using Legalpedia.Judgements.Dto;
using Legalpedia.Models;
using Legalpedia.Dto;
using Legalpedia.Dto.DataSynchronisation;

namespace Legalpedia.Judgements
{
    public class JudgementsAppService : AsyncCrudAppService<Judgement,
        JudgementDto, string,
        PagedResultRequestDto, CreateJudgementDto, UpdateJudgementDto>, IJudgementsAppService
    {
        private IJudgementRepository _repository; 

        public JudgementsAppService(IJudgementRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public JudgementDto Post(Judgement judgement)
        {
            var updateCotnent = new UpdateContentDto<SharedJudgement>();
            
            var oldRecord = Repository.FirstOrDefault(r => r.Id == judgement.Id);
            if (oldRecord != null)
            {
                throw new UserFriendlyException("Duplicate suit number");
            }
            else
            {
                judgement.CreatedAt = DateTime.Now;
                judgement.UpdatedAt = DateTime.Now;
                Repository.Insert(judgement);
                updateCotnent.IsNewRecord = true;
            }
            

            return judgement.MapTo<JudgementDto>();
        }


        public JudgementDto Put(Judgement judgement)
        {
            var oldRecord = Repository.FirstOrDefault(r => r.Id == judgement.Id);
            if (oldRecord != null)
            {
                oldRecord.Body = judgement.Body;
                oldRecord.UpdatedAt = DateTime.Now;
                Repository.Update(oldRecord);
            }
            else
            {
                throw new UserFriendlyException("Invalid suit number");
            }

            return judgement.MapTo<JudgementDto>();
        }

        public PagedResultDto<JudgementListItem> GetJudgements(GetJudgementRequest input)
        {
            if (input.Query == GetJudgementRequest.QueryNoSummary)
            {
                return _repository.GetOrphanJudgements(input);
            }
            var query = Repository.GetAll().Where(j => j.Id != "");
            if (!string.IsNullOrEmpty(input.Query))
            {
                query = query.Where(j => j.Id.ToLower().Contains(input.Query.ToLower()));
            }

            var totalCount = query.Count();
            var juedgements = query.OrderByDescending(j=>j.Id).Skip(input.SkipCount).Take(input.MaxResultCount)
                .Select(j => new JudgementListItem
                {
                    SuitNo = j.Id, CaseTitle = j.Body.Substring(0, 300)
                });
            return new PagedResultDto<JudgementListItem>(totalCount, juedgements.ToList());
        }
    }
}
