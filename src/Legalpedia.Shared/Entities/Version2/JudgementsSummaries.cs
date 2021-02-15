using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class JudgementsSummaries
    {
        public JudgementsSummaries()
        {
            CreatedAt = DateTime.Now;
        }

        [Key]
        [Column("SuitNo")]
        public string Id { get; set; }

        public string Title { get; set; }

        public string SummaryOfFacts { get; set; }

        public string Held { get; set; }

        public string Issues { get; set; }

        public string CasesCited { get; set; }

        [Column("StatutesCited")]
        public string StatusCited { get; set; }

        public string LpCitation { get; set; }

        public DateTime? JudgementDate { get; set; }

        public string OtherCitations { get; set; }

        public int? HoldenAtId { get; set; }

        public int? CourtId { get; set; }

        public int? PartyATypeId { get; set; }

        public int? PartyBTypeId { get; set; }

        public int? CategoryId { get; set; }

        public string Tags { get; set; }

        public DateTime? CreatedAt { get; set; }
        

        [ForeignKey("CourtId")]
        public Courts Court { get; set; }

        [ForeignKey("HoldenAtId")]
        public HoldenAts HoldenAt { get; set; }

        [ForeignKey("PartyATypeId")]
        public PartyATypes PartyAType { get; set; }

        [ForeignKey("PartyBTypeId")]
        public PartyBTypes PartyBType { get; set; }

        [ForeignKey("CategoryId")]
        public Categories Category { get; set; }

        [ForeignKey("Id")]
        public JudgementPartiesA PartiesAs { get; set; }

        [ForeignKey("Id")]
        public JudgementPartiesB PartiesBs { get; set; }

        [ForeignKey("Id")]
        public JudgementCounsels Counsels { get; set; }
    
        public List<SummaryRatios> Ration { get; set; }
    }
}
