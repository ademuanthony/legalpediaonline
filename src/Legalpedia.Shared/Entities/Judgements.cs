using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("judgements")]
    public class Judgements
    {
        [Key]
        [Column("uuid")]
        public string Uuid { get; set; }
        [Column("summary_uuid")]
        public string SummaryUuid { get; set; }
        [Column("judgement")]
        public string Judgement { get; set; }
        [Column("counsel")]
        public string Counsel { get; set; }
        [Column("category_tags")]
        public string CategoryTags { get; set; }
        [Column("date_posted")]
        public string DatePosted { get; set; }
        [Column("date_updated")]
        public string DateUpdated { get; set; }
    }
}
