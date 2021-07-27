using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class Annotation: Entity<string>
    {
        public long UserId { get; set; }
        public string ContentId { get; set; }
        public ContentType ContentType { get; set; }
        public Visibility Visibility { get; set; }
        public string TextTarget { get; set; }
        public string Comment { get; set; }
        public string Replies { get; set; }
        public string Tags { get; set; }
        
        public string Blob { get; set; }
    }

    public class AnnotationTag: Entity
    {
        public string Tag { get; set; }
    }

    public enum Visibility
    {
        Private = 0,
        Public = 1
    }
    
    /// <summary>
    /// ContentType for annotation
    /// Every update to this enum must be replicated on the frontend
    /// </summary>
    public enum ContentType
    {
        JudgementBody = 0,
        Lfn = 1,
        RulesOfCourt = 2,
        FormsAndPrecedents = 3,
        LpArticles = 4,
        PublicArticles = 5,
        LawDictionary = 6,
        LegalMaxims = 7,
        ForeignResources = 8
    }
}