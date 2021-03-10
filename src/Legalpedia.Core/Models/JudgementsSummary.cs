using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class JudgementsSummary:Entity<string>
    {
        public JudgementsSummary()
        {
            CreatedAt = DateTime.Now;
        }

        public string Title { get; set; }

        public string SummaryOfFacts { get; set; }

        public string Held { get; set; }

        public string Issues { get; set; }

        public string CasesCited { get; set; }

        [Column("StatutesCited")]
        public string StatusCited { get; set; }

        [Column("LPCitation")]
        public string LpCitation { get; set; }

        public DateTime? JudgementDate { get; set; }

        public string OtherCitations { get; set; }

        [Column("HoldenAtID")]
        public int? HoldenAtId { get; set; }

        [Column("CourtID")]
        public int? CourtId { get; set; }

        [Column("PartyATypeID")]
        public int? PartyATypeId { get; set; }

        [Column("PartyBTypeID")]
        public int? PartyBTypeId { get; set; }

        [Column("CategoryId")]
        public int? CategoryId { get; set; }

        public string Tags { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


        #region Navigation ppts
        [Column(nameof(HoldenAtId))]
        public HoldenAt HoldenAt { get; set; }

        [Column(nameof(CategoryId))]
        public Category Category { get; set; }

        [ForeignKey(nameof(CourtId))]
        public Court Court { get; set; }

        [ForeignKey(nameof(PartyATypeId))]
        public PartyAType PartyAType { get; set; }

        [ForeignKey(nameof(PartyBTypeId))]
        public PartyBType PartyBType { get; set; }

        //public ICollection<SummaryRatio> Ration { get; set; } 
        #endregion
    }
}
