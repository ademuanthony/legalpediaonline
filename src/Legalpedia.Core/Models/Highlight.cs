using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class Highlight: Entity<string>
    {
        public long UserId { get; set; }
        public string CollectionId { get; set; }
        public string Text { get; set; }
        public string CaseId { get; set; }
        public int PageNumber { get; set; }
        public int StartIndex { get; set; }
        public int Length { get; set; }
    }
}