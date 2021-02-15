using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("recent_downloads")]
    public class RecentDownload
    {
        [Key]
        [Column("rowid")]
        public int Int { get; set; }
        [Column("Case_Title")]
        public string CaseTitle { get; set; } 
        [Column("Court")]
        public string Court { get; set; }
        [Column("Judgement_Date")]
        public string JudgementDate { get; set; }
        [Column("Case_uuid")]
        public string CaseUuid { get; set; }
        [Column("Download_Date")]
        public DateTime DwonloadDate { get; set; }
        [Column("Date")]
        public DateTime Date { get; set; }
    }
}
