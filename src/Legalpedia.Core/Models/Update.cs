using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.Models
{
    public enum UpdateStatus
    {
        New = 1,
        Running = 2,
        Failed = 3,
        PartiallyFailed = 4,
        Completed = 5
    }
    [Table("updates")]
    public class Update
    {
        [Column("rowid")]
        public long Id { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("count")]
        public int Count { get; set; }

        [Column("status")]
        public UpdateStatus Status { get; set; }
    }
}
