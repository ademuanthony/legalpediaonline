using Abp.Authorization;
using Legalpedia.Authorization.Roles;
using Legalpedia.Authorization.Users;

namespace Legalpedia.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
