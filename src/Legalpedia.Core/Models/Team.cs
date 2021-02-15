using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("Teams")]
    public class Team : Entity
    {
        public Team()
        {
            Uuid = Guid.NewGuid().ToString();
        }
        [Key]
        [Column("uuid")]
        public string Uuid { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("logo")]
        public string Logo { get; set; }
    }
}
