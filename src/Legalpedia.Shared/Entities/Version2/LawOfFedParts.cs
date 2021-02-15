using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class LawOfFedParts
    {
        [Key]
        [Column("PartId")]
        public int Id { get; set; }

        [Column("LawId")]
        public int LawId { get; set; }

        public string PartHeader { get; set; }
          
        [ForeignKey("LawId")]
        public LawOfFederation Law { get; set; }

        public List<LawOfFedSections> Sections { get; set; }
    }
}
