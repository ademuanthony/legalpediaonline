using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    [Table("HoldenAt")]
    public class HoldenAts
    {
        [Key]
        [Column("HoldenAtID")]
        public int Id { get; set; }

        [Column("HoldenAt")]
        public string Name { get; set; }
        
    }
}
