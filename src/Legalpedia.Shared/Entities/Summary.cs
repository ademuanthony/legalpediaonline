using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("summaries")]
    public class Summary
    {
        [Column("uuid")]
        [Key]
        public string Uuid { get; set; }
        [Column("case_title")]
        public string CaseTitle { get; set; }
        [Column("area_of_law")]
        public string AreaOfLaw { get; set; }
        [Column("courts")]
        public string Courts { get; set; }
        [Column("summary_of_facts")]
        public string SummaryOfFacts { get; set; }
        [Column("held")]
        public string Held { get; set; }
        [Column("issue")]
        public string Issue { get; set; }
        [Column("ratio")]
        public string Ratio { get; set; }
        [Column("category_tags")]
        public string CategoryTags { get; set; }
        [Column("cases_cited")]
        public string CasesCitied { get; set; }
        [Column("statutes_cited")]
        public string StatusCited { get; set; }
        [Column("subject_matter")]
        public string SubjectMatter { get; set; }
        [Column("principle_uuid")]
        public string PrincipalUuid { get; set; }
        [Column("case_id")]
        public string CaseId { get; set; }
        [Column("case_uuid")]
        public string CaseUuid { get; set; }
        [Column("judgement_date")]
        public string JudgementDate { get; set; }
        [Column("l_citation")]
        public string ICitation { get; set; }
        [Column("o_citations")]
        public string OCitations { get; set; }
        [Column("sitting_at")]
        public string SittingAt { get; set; }
        [Column("suit_number")]
        public string SuitNumber { get; set; }
        [Column("coram")]
        public string Coram { get; set; }
        [Column("party_a_type")]
        public string PartyAType { get; set; }
        [Column("party_a_names")]
        public string PartyAName { get; set; }
        [Column("party_b_type")]
        public string PartyBType { get; set; }
        [Column("party_b_names")]
        public string PartyBName { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("judge_month")]
        public string JudgeMonth { get; set; }
        [Column("date_posted")]
        public string DatePosted { get; set; }
        [Column("date_updated")]
        public string DateUpdated { get; set; }
    }
}
