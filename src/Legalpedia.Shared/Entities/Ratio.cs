using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    public class Ratio
    {
        [Column("id")]
        [Key]
        public long Id { get; set; }
        [Column("uuid")]
        public string Uuid { get; set; }
        [Column("summary_uuid")]
        public string SummaryUuid { get; set; }
        [Column("category_tags")]
        public string CategoryTags { get; set; }
        [Column("head")]
        public string Head { get; set; }
        [Column("body")]
        public string Body { get; set; }
        [Column("case_title")]
        public string CaseTitle { get; set; }
        [Column("courts")]
        public string Courts { get; set; }
        [Column("judgement_date")] 
        public string JudgementDate { get; set; }
    }
}
