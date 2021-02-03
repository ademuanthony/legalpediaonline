using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Legalpedia.Models;
using Legalpedia.Parties.PartyTypeBs.Dto;

namespace Legalpedia.Parties.PartyTypeBs
{
    public class PartyBTypesAppService : AsyncCrudAppService<PartyBType,
        PartyBTypeDto, int,
        PagedResultRequestDto, CreatePartyBTypeDto, UpdatePartyBTypeDto>, IPartyBTypesAppService
    {
        public PartyBTypesAppService(IRepository<PartyBType> repository) : base(repository)
        {

        }
    }
}
