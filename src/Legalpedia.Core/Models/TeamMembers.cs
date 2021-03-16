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
        [ForeignKey("Team")]
        public string TeamId { get; set; }
        
        [ForeignKey("User")]
        public long UserId { get; set; }
        
        public TeamRole Role { get; set; }

        public Team Team { get; set; }
        public User User { get; set; }
    }

    public enum TeamRole
    {
        Member,
        Admin
    }
}
