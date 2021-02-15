using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("keyword_rankings")]
    public class KeywordRanking
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("keyword_id")]
        public long KeywordId { get; set; }
       
        [Column("rank")]
        public long Rank { get; set; }

        [Column("index_id")]
        public long IndexId { get; set; }

        //[ForeignKey("index_id")]
        //public Index Index { get; set; }

        [Column("uuid")]
        public string Uuid { get; set; }
/*
        [Column("ref_uuid")]
        public string RefUuid { get; set; }*/

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
