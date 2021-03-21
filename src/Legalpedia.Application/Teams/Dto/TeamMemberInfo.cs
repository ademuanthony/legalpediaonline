using Legalpedia.Models;

namespace Legalpedia.Teams.Dto
{
    public class TeamMemberInfo
    {
        public long UserId { get; set; }
        public string TeamId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public TeamRole Role { get; set; }
    }
}