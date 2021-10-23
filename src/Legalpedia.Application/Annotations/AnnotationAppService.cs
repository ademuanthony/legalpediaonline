using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
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
            input.CreationDate = DateTime.Now;
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

        public List<Annotation> PublishedNotes()
        {
            return _repository.GetAll()
                .Where(an => an.UserId == AbpSession.UserId.Value && an.Comment != "")
                .OrderByDescending(an =>an.CreationDate)
                .ToList();
        }

        public PagedResultDto<Annotation> Search(SearchAnnotationInput input)
        {
            var items = _repository.GetAll().Where(
                    an => (an.Comment.ToLower().Contains(input.Term.ToLower()) ||
                           an.Replies.ToLower().Contains(input.Term.ToLower())) &&
                          (an.UserId == AbpSession.UserId.Value ||
                           an.Visibility == Visibility.Public)).OrderBy(an=>an.Comment.Length)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();

            var totalCount = _repository.GetAll().Count(
                an => (an.Comment.ToLower().Contains(input.Term.ToLower()) ||
                       an.Replies.ToLower().Contains(input.Term.ToLower())) &&
                      (an.UserId == AbpSession.UserId.Value ||
                       an.Visibility == Visibility.Public));

            return new PagedResultDto<Annotation>(totalCount, items);
        }

        public List<string> Tags()
        {
            return _tagRepository.GetAll().Select(t=>t.Tag).ToList();
        }
    }
}