using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.UI;
using Legalpedia.Models;

namespace Legalpedia.Annotations
{
    public class AnnotationAppService: LegalpediaAppServiceBase, IAnnotationAppService
    {
        private readonly IRepository<Annotation, string> _repository;

        public AnnotationAppService(IRepository<Annotation, string> repository)
        {
            _repository = repository;
        }

        public Annotation Create(Annotation input)
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

        public List<Annotation> GetAll(string caseId)
        {
            return _repository.GetAll()
                .Where(h => h.CaseId == caseId && h.UserId == AbpSession.UserId.Value)
                .OrderByDescending(h=>h.StartIndex)
                .ToList();
        }

        public List<Annotation> GetByCollectionId(string collectionId)
        {
            return _repository.GetAll().Where(h => h.CollectionId == collectionId).ToList();
        }

        public List<Annotation> Search(string term)
        {
            return _repository.GetAll().Where(an => an.Note.ToLower().Contains(term.ToLower()))
                .ToList();
        }
    }
}