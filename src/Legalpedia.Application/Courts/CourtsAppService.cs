using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Legalpedia.Courts.Dto;
using Legalpedia.Models;

namespace Legalpedia.Courts
{
    public class CourtsAppService : AsyncCrudAppService<Court,
        CourtDto, int,
        PagedResultRequestDto, CreateCourtDto, UpdateCourtDto>, ICourtsAppService
    {
        public CourtsAppService(IRepository<Court> repository) : base(repository)
        {

        }
    }
}
