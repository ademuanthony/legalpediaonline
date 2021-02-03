using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("AreasOfLaw")]
    public class AreaOfLaw:Entity
    {
        //[Key]
        //[Column("AreaOfLawID")]
        //public override int Id { get; set; }

        [Column("AreaOfLaw")]
        public string Title { get; set; }

    }
}
