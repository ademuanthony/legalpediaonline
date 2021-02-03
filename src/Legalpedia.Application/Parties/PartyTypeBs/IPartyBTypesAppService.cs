using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Parties.PartyTypeBs.Dto;

namespace Legalpedia.Parties.PartyTypeBs
{
    public interface IPartyBTypesAppService : IAsyncCrudAppService<PartyBTypeDto, int,
        PagedResultRequestDto, CreatePartyBTypeDto, UpdatePartyBTypeDto>
    {
    }
}
