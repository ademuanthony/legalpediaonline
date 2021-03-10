using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("SummaryRatios")]
    public class SummaryRatio:Entity
    {
        //[Key]
        //[Column("RatioID")]
        //public override int Id { get; set; }

        public string Heading { get; set; }

        public string Body { get; set; }

        public string SuitNo { get; set; }

        public string Coram { get; set; }

        [NotMapped]
        public List<string> Smis { get; set; }

    }
}
