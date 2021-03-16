using Legalpedia.Models;

namespace Legalpedia.Teams.Dto
{
    public class ChangeRoleInput
    {
        public string TeamMemberId { get; set; }
        
        public TeamRole Role { get; set; }
    }
}