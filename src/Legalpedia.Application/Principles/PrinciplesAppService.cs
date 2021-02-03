using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Legalpedia.Models;
using Legalpedia.Principles.Dto;

namespace Legalpedia.Principles
{
    public class PrinciplesAppService : AsyncCrudAppService<Principle,
        PrincipleDto, int,
        PagedResultRequestDto, CreatePrincipleDto, UpdatePrincipleDto>, IPrinciplesAppService
    {
        public PrinciplesAppService(IRepository<Principle> repository) : base(repository)
        {
        }
    }
}
