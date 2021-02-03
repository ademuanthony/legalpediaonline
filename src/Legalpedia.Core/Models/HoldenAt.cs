using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("HoldenAt")]
    public class HoldenAt:Entity
    {
        //[Key]  //HoldenAtID
        //[Column("HoldenAtID")]
        //public override int Id { get; set; }

        [Column("HoldenAt")]
        public string Name { get; set; }

    }
}
