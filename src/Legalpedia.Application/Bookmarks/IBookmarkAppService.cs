using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Domain.Entities;
using Legalpedia.Bookmarks.Dto;
using Legalpedia.Dictionaries.Dtos;
using Legalpedia.Models;

namespace Legalpedia.Bookmarks
{
    public interface IBookmarkAppService: IApplicationService
    {
        public BookmarkCollection CreateCollection(BookmarkCollection input);
        public List<BookmarkCollection> MyCollections();
        public void DeleteCollection(Entity<string> input);
        
        public Bookmark Create(Bookmark input);
        public List<Bookmark> GetAll(GetAllInput input);
        public List<Bookmark> GetByCollectionId(Entity<string> input);
        public void Kickout(string id);
    }
}