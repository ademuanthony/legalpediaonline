using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class LawOfFedSections
    {
        [Key]
        [Column("SectionId")]
        public int Id { get; set; }

        public string SectionHeader { get; set; }

        public string SectionBody { get; set; }

        public int LawId { get; set; }

        public int PartId { get; set; }

        [ForeignKey("PartId")]
        public LawOfFedParts Part { get; set; }
    }
}
