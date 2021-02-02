using System.Threading.Tasks;
using Legalpedia.Configuration.Dto;

namespace Legalpedia.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
