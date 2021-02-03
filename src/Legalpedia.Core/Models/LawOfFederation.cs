using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("LawsOfFederation")]
    public class LawOfFederation:Entity
    {
        //[Key]
        //[Column("LawId")]
        //public override int Id { get; set; }

        public int? CatId { get; set; }

        public string LawNo { get; set; }

        public string Title { get; set; }

        public DateTime LawDate { get; set; }

        public string Descr { get; set; }

        public string SubsidiaryLegislation { get; set; }

        public string Tags { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [NotMapped]
        public List<LawOfFedPart> Parts { get; set; }

        [NotMapped]
        public List<LawOfFedSched> Schedules { get; set; }

    }
}
