using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Courts.Dto;

namespace Legalpedia.Courts
{
    public interface ICourtsAppService : IAsyncCrudAppService<CourtDto, int,
        PagedResultRequestDto, CreateCourtDto, UpdateCourtDto>
    {
    }
}
