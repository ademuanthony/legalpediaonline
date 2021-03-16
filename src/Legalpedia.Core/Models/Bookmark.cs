using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class Bookmark: Entity<string>
    {
        public long UserId { get; set; }
        public string CollectionId { get; set; }
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public string CaseId { get; set; }
    }
}