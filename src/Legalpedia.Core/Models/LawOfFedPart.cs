using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class LawOfFedPart:Entity
    {
        //[Key]
        //[Column("PartId")]
        //public override int Id { get; set; }

        public int LawId { get; set; }

        public string PartHeader { get; set; }

        [NotMapped]
        public List<LawOfFedSection> Sections { get; set; }
    }
}
