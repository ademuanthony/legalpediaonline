using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Legalpedia.Models
{
    [Table("Teams")]
    public class Team : Entity<string>, ICreationAudited
    {
        public long? CreatorUserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public DateTime CreationTime { get; set; }
    }
}
