using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    public class Synchronization
    {
        [Key]
        [Column("ClientId")]
        public string Id { get; set; }

        public DateTime LastExported { get; set; }

        public DateTime LastImported { get; set; }

        public string LastSynchronization { get; set; }
    }
}
