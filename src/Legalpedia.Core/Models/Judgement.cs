using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class Judgement:Entity<string>
    {
        public Judgement()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        //[Key]
        //[ Column("SuitNo")]
        //public override string Id { get; set; }

        [Column("judgement")]
        public string Body { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}
