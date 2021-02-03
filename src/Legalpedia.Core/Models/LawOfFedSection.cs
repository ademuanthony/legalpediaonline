using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class LawOfFedSection:Entity
    {
        //[Key]
        //[Column("SectionId")]
        //public override int Id { get; set; }

        public string SectionHeader { get; set; }

        public string SectionBody { get; set; }

        public int LawId { get; set; }

        public int PartId { get; set; }
       
    }
}
