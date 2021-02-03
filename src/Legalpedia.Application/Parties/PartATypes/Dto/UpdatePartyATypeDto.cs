using Abp.Application.Services.Dto;

//using Legalbackend.Legalpedia.Version2;

namespace Legalpedia.Parties.PartATypes.Dto
{
    //[AutoMapTo(typeof(PartyATypes))]
    public class UpdatePartyATypeDto:EntityDto
    {
        public string PartyAType { get; set; }
    }
}
