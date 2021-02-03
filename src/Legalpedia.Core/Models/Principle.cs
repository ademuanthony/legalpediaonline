using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class Principle:Entity
    {
        //[Key]
        //[Column("PrincipleId")]
        //public override int Id { get; set; }

        [Column("Principle")]
        public string Name { get; set; }

        public int? SbjMatterIdxId { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
