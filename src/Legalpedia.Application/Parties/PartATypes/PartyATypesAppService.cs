using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Legalpedia.Models;
using Legalpedia.Parties.PartATypes.Dto;

namespace Legalpedia.Parties.PartATypes
{
    public class PartyATypesAppService : AsyncCrudAppService<PartyAType,
        PartyATypeDto, int,
        PagedResultRequestDto, CreatePartyATypeDto, UpdatePartyATypeDto>, IPartyATypesAppService
    {
        public PartyATypesAppService(IRepository<PartyAType> repository) : base(repository)
        {

        }
    }
}
