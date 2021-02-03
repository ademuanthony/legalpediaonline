using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Parties.PartyTypeBs.Dto
{
    [AutoMapFrom(typeof(PartyBType))]
    public class PartyBTypeDto:EntityDto
    {
        public string PartyBType { get; set; }
    }
}
