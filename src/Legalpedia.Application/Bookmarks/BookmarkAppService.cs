using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.UI;
using Legalpedia.Bookmarks.Dto;
using Legalpedia.Models;

namespace Legalpedia.Bookmarks
{
    [AbpAuthorize]
    public class BookmarkAppService: LegalpediaAppServiceBase, IBookmarkAppService
    {
        private readonly IRepository<BookmarkCollection, string> _collectionRepository;
        private readonly IRepository<Bookmark, string> _repository;

        public BookmarkAppService(IRepository<BookmarkCollection, string> collectionRepository, IRepository<Bookmark, string> repository)
        {
            _collectionRepository = collectionRepository;
            _repository = repository;
        }

        [AbpAuthorize]
        public BookmarkCollection CreateCollection(BookmarkCollection input)
        {
            if (_collectionRepository.Count(c => c.UserId == AbpSession.UserId.Value &&
                                                 c.Name == input.Name) > 0)
            {
                throw new UserFriendlyException("A collection with the same name already exists");
            }
            
            if (AbpSession.UserId != null) input.UserId = AbpSession.UserId.Value;
            input.Id = Guid.NewGuid().ToString();
            _collectionRepository.Insert(input);
            return input;
        }

        [AbpAuthorize]
        public List<BookmarkCollection> MyCollections()
        {
            var collections = _collectionRepository.GetAll()
                .Where(c => c.UserId == AbpSession.UserId.Value).ToList();

            if (collections.Count != 0) return collections;
            if (AbpSession.UserId == null) return collections;
            var collection = new BookmarkCollection
            {
                UserId = AbpSession.UserId.Value,
                Name = "Main",
                Id = Guid.NewGuid().ToString()
            };
            _collectionRepository.Insert(collection);
            collections.Add(collection);

            return collections;
        }

        [AbpAuthorize]
        public void DeleteCollection(Entity<string> input)
        {
            var collection = _collectionRepository.FirstOrDefault(c => c.Id == input.Id);
            if (collection == null) throw new UserFriendlyException("Collection not found");
            if (AbpSession.UserId != null && collection.UserId != AbpSession.UserId.Value)
            {
                throw new UserFriendlyException("You cannot delete a collection that you didn't create");
            }
            _repository.Delete(b=>b.CollectionId == input.Id);
            _collectionRepository.Delete(collection);
        }

        #region Bookmarks
        [AbpAuthorize]
        public Bookmark Create(Bookmark input)
        {
            if (AbpSession.UserId != null) input.UserId = AbpSession.UserId.Value;
            input.Id = Guid.NewGuid().ToString();
            _repository.Insert(input);
            return input;
        }

        public List<Bookmark> GetAll(GetAllInput input)
        {
            var bookmarks = _repository.GetAll().Where(b => b.UserId == AbpSession.UserId.Value 
                                                   && b.CaseId == input.CaseId)
                .ToList();
            return bookmarks;
        }

        public List<Bookmark> GetByCollectionId(Entity<string> input)
        {
            return _repository.GetAll().Where(b => b.CollectionId == input.Id).ToList();
        }

        public void Kickout(string id)
        {
            var bookmark = _repository.FirstOrDefault(b => b.Id == id);
            if (bookmark == null)
            {
                throw new UserFriendlyException("Bookmark not found");
            }

            if (AbpSession.UserId != null && bookmark.UserId != AbpSession.UserId.Value)
            {
                throw new UserFriendlyException("This is not your bookmark");
            }
            _repository.Delete(bookmark);
        }

        #endregion
        
    }
}