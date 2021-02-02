using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Legalpedia.Controllers
{
    public abstract class LegalpediaControllerBase: AbpController
    {
        protected LegalpediaControllerBase()
        {
            LocalizationSourceName = LegalpediaConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
