using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public enum IndexType
    {
        None = 0,
        Ratio = 1,
        Summary = 2
    }
    [Table("indexes")]
    public class Index : Entity<long>
    {
        [Key]
        [Column("rowid")]
        public override long Id { get; set; }

        [Column("uuid")]
        public string Uuid { get; set; }
        
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
