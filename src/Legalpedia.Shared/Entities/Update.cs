using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
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
        public Update()
        {
            LastDownloadDate = DateTime.Now;
        }
        [Column("rowid")]
        public long Id { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("count")]
        public int Count { get; set; }

        [Column("downloaded")]
        public int Downloaded { get; set; }

        [Column("status")]
        public UpdateStatus Status { get; set; }

        [Column("last_download_date")]
        public DateTime? LastDownloadDate { get; set; }

        [Column("versionDate")]
        public DateTime VersionDate { get; set; }

        public DateTime CheckDate { get; set; }
    }
}
