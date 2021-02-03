using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Parties.PartyTypeBs.Dto
{
    [AutoMapTo(typeof(PartyBType))]
    public class CreatePartyBTypeDto
    {
        public string PartyBType { get; set; }
    }
}
