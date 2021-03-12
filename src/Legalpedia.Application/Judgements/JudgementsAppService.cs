using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
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
        private readonly IJudgementRepository _repository;
        private readonly IRepository<JudgementPage, string> _pageRepositry;

        public JudgementsAppService(IJudgementRepository repository, IRepository<JudgementPage, string> pageRepositry)
            : base(repository)
        {
            _repository = repository;
            _pageRepositry = pageRepositry;
        }

        public JudgementDto Post(JudgementDto judgement)
        {
            var updateCotnent = new UpdateContentDto<SharedJudgement>();
            
            var oldRecord = Repository.FirstOrDefault(r => r.Id == judgement.Id);
            if (oldRecord != null)
            {
                throw new UserFriendlyException("Duplicate suit number");
            }
            else
            {
                Repository.Insert(new Judgement
                {
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Body = judgement.Body,
                    Id = judgement.Id
                });
                updateCotnent.IsNewRecord = true;
            }

            foreach (var page in judgement.Pages)
            {
                page.SuitNumber = judgement.Id;
                _pageRepositry.Insert(page);
            }
            

            return judgement.MapTo<JudgementDto>();
        }


        public JudgementDto Put(JudgementDto judgement)
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
            _pageRepositry.Delete(p => p.SuitNumber == judgement.Id);
            foreach (var page in judgement.Pages)
            {
                page.SuitNumber = judgement.Id;
                _pageRepositry.Insert(page);
            }
            return judgement.MapTo<JudgementDto>();
        }

        public bool PaginateAll()
        {
            var ids = _repository.GetAll().Select(s => s.Id).ToList();
            return ids.All(id => Paginate(new EntityDto<string>(id)));
        }
        public bool Paginate(EntityDto<string> input)
        {
            var judgement = Repository.FirstOrDefault(j => j.Id == input.Id);
            if (judgement == null)
            {
                throw new UserFriendlyException("Case not found");
            }
            
            _pageRepositry.Delete(p=>p.SuitNumber == input.Id); // TODO: make this explicit

            var body = "";
            var pages = judgement.Body.Split("PAGE|");
            foreach (var page in pages)
            {
                var lines = page.Split("\r\n").ToList();
                var fLine = lines[0].Trim('\r', ' ');
                if (fLine.Length <= 2 && int.TryParse(fLine, out var _))
                {
                    lines.RemoveAt(0);
                }

                lines = lines.Where(l => !l.IsNullOrEmpty()).ToList();
                body += " " + string.Join("\r\n", lines.ToArray());
            }

            body = body.Trim();
            body = body.Replace("\r\n\r\n", "\r\n");
            var words = body.Split(' ');
            int pageNumer = 0;
            var l = 250;
            for (var i = 0; i < words.Length; i+=l)
            {
                if (i+l > words.Length)
                {
                    l = words.Length - i;
                }
                var pageWords = new string[l];
                Array.Copy(words, i, pageWords, 0, l);
                var content = string.Join(' ', pageWords);
                _pageRepositry.Insert(new JudgementPage
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = content,
                    SuitNumber = input.Id,
                    Number = ++pageNumer
                });
            }
            return true;
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

        public List<JudgementPage> GetPages(EntityDto<string> entityDto)
        {
            return _pageRepositry.GetAll().Where(p => p.SuitNumber == entityDto.Id).ToList();
        }
    }
}
