using System.Threading.Tasks;
using Abp.Application.Services;
using Legalpedia.Sessions.Dto;

namespace Legalpedia.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
