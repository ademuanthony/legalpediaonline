using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("SumAreasOfLaw")]
    public class SumAreasOfLaw:Entity
    {
        //[Key]
        //[Column("SumAreaOfLawID")]
        //public override int Id { get; set; }

        public string SuitNo { get; set; }

        public int AreaOfLawID { get; set; }

        [ForeignKey("AreaOfLawID")]
        public AreaOfLaw AreaOfLaw { get; set; }

    }
}
