namespace Legalpedia.Shared.Dto.Entities
{
    public class Judgements
    {
        public long Id { get; set; }
        public string Uuid { get; set; }
        public string SummaryUuid { get; set; }
        public string Judgement { get; set; }
        public string Counsel { get; set; }
        public string CategoryTags { get; set; }
        public string DatePosted { get; set; }
        public string DateUpdated { get; set; }
    }
}
