using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Legalpedia.Corams.Dto;
using Legalpedia.Models;

namespace Legalpedia.Corams
{
    public class CoramsAppService : AsyncCrudAppService<Coram,
        CoramDto, int,
        PagedResultRequestDto, CreateCoramDto, UpdateCoramDto>, ICoramsAppService
    {
        public CoramsAppService(IRepository<Coram> repository) : base(repository)
        {

        }
    }
}
