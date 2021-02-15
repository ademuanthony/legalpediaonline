using Abp.Domain.Entities;
using Legalpedia.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.Models
{
    [Table("TeamMembers")]
    public class TeamMember : Entity
    {
        public TeamMember()
        {
            Uuid = Guid.NewGuid().ToString();
        }
        [Key]
        [Column("uuid")]
        public string Uuid { get; set; }
        [Column("teamuuid")]
        [ForeignKey("Team")]
        public string TeamUuid { get; set; }
        [Column("userid")]
        [ForeignKey("User")]
        public long UserId { get; set; }

        public Team Team { get; set; }
        public User User { get; set; }


    }
}
