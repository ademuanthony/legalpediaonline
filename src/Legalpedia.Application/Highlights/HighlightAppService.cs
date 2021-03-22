using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Legalpedia.Models;

namespace Legalpedia.Highlights
{
    [AbpAuthorize]
    public class HighlightAppService: LegalpediaAppServiceBase, IHighlightAppService
    {
        private readonly IRepository<Highlight, string> _repository;

        public HighlightAppService(IRepository<Highlight, string> repository)
        {
            _repository = repository;
        }

        public Highlight Create(Highlight input)
        {
            if (AbpSession.UserId != null) input.UserId = AbpSession.UserId.Value;
            input.Id = Guid.NewGuid().ToString();
            _repository.Insert(input);
            return input;
        }

        public void Clear(string id)
        {
            var highlight = _repository.FirstOrDefault(h => h.Id == id);
            if (highlight == null)
            {
                throw new UserFriendlyException("Highlight not found");
            }

            if (AbpSession.UserId != null && highlight.UserId != AbpSession.UserId.Value)
            {
                throw new UserFriendlyException("You cannot clear this highlight. You didn't create it");
            }
            _repository.Delete(highlight);
        }

        public List<Highlight> GetAll(string caseId)
        {
            return _repository.GetAll()
                .Where(h => h.CaseId == caseId && h.UserId == AbpSession.UserId.Value)
                .OrderByDescending(h=>h.StartIndex)
                .ToList();
        }

        public List<Highlight> GetByCollectionId(string collectionId)
        {
            return _repository.GetAll().Where(h => h.CollectionId == collectionId).ToList();
        }
    }
}