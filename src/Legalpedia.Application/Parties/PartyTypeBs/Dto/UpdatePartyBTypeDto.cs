using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Parties.PartyTypeBs.Dto
{
    [AutoMapTo(typeof(PartyBType))]
    public class UpdatePartyBTypeDto:EntityDto
    {
        public string PartyBType { get; set; }

    }
}
