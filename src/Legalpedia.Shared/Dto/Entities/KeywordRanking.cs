using Legalpedia.DAL.Entities;

namespace Legalpedia.Shared.Dto.Entities
{
    public class KeywordRanking
    {
        public long Id { get; set; }

        public long KeywordId { get; set; }
       
        public long Rank { get; set; }

        public long IndexId { get; set; }

        public string Uuid { get; set; }

        public string Title { get; set; }

        public string Header { get; set; }

        public string Court { get; set; }

        public string JudgeDate { get; set; }

        public string Subbody { get; set; }

        public IndexType Type { get; set; }
    }
}
