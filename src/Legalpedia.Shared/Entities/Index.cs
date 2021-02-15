using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    public enum IndexType
    {
        None = 0,
        Ratio = 1,
        Summary = 2
    }
    [Table("indexes")]
    public class Index
    {
        [Key]
        [Column("rowid")]
        public long Id { get; set; }

        [Column("uuid")]
        public string SuitNo { get; set; }
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
