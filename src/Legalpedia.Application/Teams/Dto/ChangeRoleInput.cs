using Legalpedia.Models;

namespace Legalpedia.Teams.Dto
{
    public class ChangeRoleInput
    {
        public long UserId { get; set; }
        
        public string TeamId { get; set; }
        
        public TeamRole Role { get; set; }
    }
}