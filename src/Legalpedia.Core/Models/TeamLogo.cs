using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class TeamLogo: Entity<string>
    {
        public string TeamId { get; set; }
        public string Base64 { get; set; }
    }
}