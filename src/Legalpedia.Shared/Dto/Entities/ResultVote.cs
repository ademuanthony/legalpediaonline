namespace Legalpedia.Shared.Dto.Entities
{
    public class ResultVote
    {
        public long Id { get; set; }

        public long KeywordId { get; set; }

        public string IndexUuid { get; set; }

        public long Vote { get; set; }
    }
}
