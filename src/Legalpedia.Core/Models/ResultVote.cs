using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.Models
{
    [Table("result_votes")]
    public class ResultVote
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("keyword_id")]
        public long KeywordId { get; set; }

        [Column("index_uuid")]
        public string IndexUuid { get; set; }

        [Column("vote")]
        public long Vote { get; set; }
    }
}
