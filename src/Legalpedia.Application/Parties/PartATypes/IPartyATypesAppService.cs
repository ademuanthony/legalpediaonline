using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Parties.PartATypes.Dto;

namespace Legalpedia.Parties.PartATypes
{
    public interface IPartyATypesAppService : IAsyncCrudAppService<PartyATypeDto, int,
        PagedResultRequestDto, CreatePartyATypeDto, UpdatePartyATypeDto>
    {
    }
}
