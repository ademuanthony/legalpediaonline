using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class Highlight: Entity<string>
    {
        public long UserId { get; set; }
        public string CollectionId { get; set; }
        public string ContentId { get; set; }
        public string SectionId { get; set; }
        public ContentType ContentType { get; set; }
        public int PageNumber { get; set; }
        public int StartIndex { get; set; }
        public int Length { get; set; }
    }

    public enum ContentType
    {
        JudgementBody = 0,
        SummaryOfFacts = 1,
        Held = 2,
        IssuesOfDetermination = 3,
        Ration = 4,
        StatusReferred = 5,
        Lfn = 6,
        RulesOfCourt = 7,
        FormsAndPrecedents = 8,
        LpArticles = 9,
        PublicArticles = 10,
        LawDictionary = 11,
        LegalMaxims = 12,
        ForeignResources = 13
    }
}