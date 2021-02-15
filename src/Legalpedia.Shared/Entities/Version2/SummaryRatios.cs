using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    /// <summary>
    /// Ration table is a full-text indexed table for the search function
    /// CREATE virtual TABLE Ration using FTS3( 
    /// Heading,
    /// Body,
    /// SuitNo
    /// )
    /// the ration table is a warehouse for the summaryRatios table
    /// insert into Ration(Heading, Body, SuitNo) select Heading, Body, SuitNo from SummaryRatios
    /// </summary>
    //[Table("Ration")]
    [Serializable]
    public class SummaryRatios  
    {
        [Key]
        [Column("RatioID")]
        public int Id { get; set; }

        public string Heading { get; set; }

        public string Body { get; set; }

        public string SuitNo { get; set; }


        [ForeignKey("SuitNo")]
        public JudgementsSummaries Summaries { get; set; }

        public string Coram { get; set; }

        public List<string> Smis { get; set; }
    }
}
