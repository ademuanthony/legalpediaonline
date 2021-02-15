namespace Legalpedia.Shared.Dto.Entities
{
    public class Law
    {
        public long Id { get; set; }
        public string Uuid { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }
}
