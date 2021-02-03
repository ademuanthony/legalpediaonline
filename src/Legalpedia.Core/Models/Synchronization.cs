using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class Synchronization:Entity<string>
    {
        [Key]
        [Column("ClientId")]
        public override string Id { get; set; }

        public DateTime LastExported { get; set; }

        public DateTime LastImported { get; set; }

        public string LastSynchronization { get; set; }
    }
}
