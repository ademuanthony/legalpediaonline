using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("Teams")]
    public class Team : Entity<string>
    {
        public long CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
