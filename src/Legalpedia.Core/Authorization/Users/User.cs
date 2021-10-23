using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;

namespace Legalpedia.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public string Bio { get; set; }
        public string Twitter{ get; set; }
        public string Linedin { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string AreaOfPractice { get; set; }
        public string Website { get; set; }
        public int CallToBarYear { get; set; }
        
        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
    }

    public class UserDetail{
        public string Bio { get; set; }
        public string Twitter{ get; set; }
        public string Linedin { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string AreaOfPractice { get; set; }
        public string Website { get; set; }
        public int CallToBarYear { get; set; }
    }
}
