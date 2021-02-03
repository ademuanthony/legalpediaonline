using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("keyword_rankings")]
    public class KeywordRanking: Entity
    {
        [Key]
        [Column("id")]
        public override int Id { get; set; }

        [Column("keyword_id")]
        public long KeywordId { get; set; }
       
        [Column("rank")]
        public long Rank { get; set; }

        [Column("index_id")]
        public long IndexId { get; set; }

        public string SuitNo { get; set; }
        
        [Column("title")]
        public string Title { get; set; }

        [Column("header")]
        public string Header { get; set; }

        [Column("court")]
        public string Court { get; set; }

        [Column("judgedate")]
        public string JudgeDate { get; set; }

        [Column("subbody")]
        public string Subbody { get; set; }

        [Column("type")]
        public IndexType Type { get; set; }
    }
}
