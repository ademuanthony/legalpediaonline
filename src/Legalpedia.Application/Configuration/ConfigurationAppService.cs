using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Legalpedia.Configuration.Dto;

namespace Legalpedia.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : LegalpediaAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
