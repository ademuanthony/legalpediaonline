using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table(("SbjMatterIndex"))]
    public class SbjMatterIndex:Entity
    {
        //[Key]
        //[Column("SbjMatterIdxId")]
        //public override int Id { get; set; }

        public string SubjectMatterIndex { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}
