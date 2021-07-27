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
        private readonly IRepository<AnnotationTag> _tagRepository;

        public AnnotationAppService(IRepository<Annotation, string> repository, 
            IRepository<AnnotationTag> tagRepository)
        {
            _repository = repository;
            _tagRepository = tagRepository;
        }

        public Annotation Create(Annotation input)
        {
            if (AbpSession.UserId != null) input.UserId = AbpSession.UserId.Value;
            // input.Id = Guid.NewGuid().ToString();
            _repository.Insert(input);
            var tags = input.Tags.Split("|");
            foreach (var tag in tags)
            {
                if (!_tagRepository.GetAll().Any(t => t.Tag.ToLower() == tag.ToLower()))
                {
                    _tagRepository.Insert(new AnnotationTag{Tag = tag});
                }
            }
            return input;
        }

        public Annotation Update(Annotation input)
        {
            var annotation = _repository.FirstOrDefault(an => an.Id == input.Id);
            if (annotation == null)
            {
                throw new UserFriendlyException("Highlight not found");
            }
            if (input.Visibility != Visibility.Public && annotation.UserId != AbpSession.UserId)
            {
                throw new UserFriendlyException("Access denied");
            }

            annotation.Blob = input.Blob;
            annotation.Comment = input.Comment;
            annotation.Replies = input.Replies;
            annotation.Tags = input.Tags;
            annotation.Visibility = input.Visibility;
            _repository.Update(annotation);
            var tags = input.Tags.Split("|");
            foreach (var tag in tags)
            {
                if (!_tagRepository.GetAll().Any(t => t.Tag.ToLower() == tag.ToLower()))
                {
                    _tagRepository.Insert(new AnnotationTag{Tag = tag});
                }
            }
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

        public List<Annotation> GetAll(string contentId, ContentType contentType)
        {
            return _repository.GetAll()
                .Where(an => an.ContentId == contentId &&
                            (an.UserId == AbpSession.UserId.Value || an.Visibility == Visibility.Public) &&
                            an.ContentType == contentType)
                .ToList();
        }

        public List<Annotation> Search(string term)
        {
            return _repository.GetAll().Where(
                    an => (an.Comment.ToLower().Contains(term.ToLower()) ||
                           an.Replies.ToLower().Contains(term.ToLower())) &&
                          (an.UserId == AbpSession.UserId.Value ||
                           an.Visibility == Visibility.Public))
                .ToList();
        }

        public List<string> Tags()
        {
            return _tagRepository.GetAll().Select(t=>t.Tag).ToList();
        }
    }
}