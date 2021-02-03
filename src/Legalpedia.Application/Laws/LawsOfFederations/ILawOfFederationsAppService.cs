using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Laws.LawsOfFederations.Dtos;
using Legalpedia.Models;
using Legalpedia.Dto;

namespace Legalpedia.Laws.LawsOfFederations
{
    public interface ILawsOfFederationAppService : IAsyncCrudAppService<LawOfFederationDto, int,
        PagedResultRequestDto, CreateLawOfFederationDto, UpdateLawOfFederationDto>
    {
        PagedResultDto<LawOfFederationDto> Filter(FilterInput input);
        PagedResultDto<LawSearchResult> Search(FilterInput input);
        LawOfFederation Detail(EntityDto input);
        bool Post(LawOfFederation model);
        bool Put(LawOfFederation model);
    }
}
