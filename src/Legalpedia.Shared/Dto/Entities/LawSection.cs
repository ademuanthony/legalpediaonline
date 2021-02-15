namespace Legalpedia.Shared.Dto.Entities
{
    public class LawSection
    {
        public long Id { get; set; }
        public string Uuid { get; set; }
        public string LawUuid { get; set; }
        public string PartScedule { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
