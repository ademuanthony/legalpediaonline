using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CsQuery.ExtensionMethods;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    [Table("LawsOfFederation")]
    public class LawOfFederation
    {
        [Key]
        [Column("LawId")]
        public int Id { get; set; }

        public int? CatId { get; set; }

        public string LawNo { get; set; }

        public string Title { get; set; }

        public DateTime LawDate { get; set; }

        public string LawDateString => LawDate.ToLongDateString();

        public string Descr { get; set; }

        public string SubsidiaryLegislation { get; set; }

        public string Tags { get; set; }
        
        public List<LawOfFedParts> Parts { get; set; }

        [NotMapped]
        public List<LawOfFedScheds> Schedules { get; set; }

        [NotMapped]
        public List<LawOfFedSections> Sections
        {
            get
            {
                var sections = new List<LawOfFedSections>();
                if (Parts == null) return sections;
                foreach (var part in Parts)
                {
                    sections.AddRange(part.Sections);
                }

                return sections;
            }
        }
    }
}
