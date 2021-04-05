using Abp.Domain.Entities;

namespace Legalpedia.Authorization.Users
{
    public class UserPicture: Entity<string>
    {
        public long UserId { get; set; }
        public string Content { get; set; }
    }
}