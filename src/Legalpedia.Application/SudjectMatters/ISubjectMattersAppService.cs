using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Principles.Dto;
using Legalpedia.SudjectMatters.Dto;

namespace Legalpedia.SudjectMatters
{
    public interface ISubjectMattersAppService : IAsyncCrudAppService<SubjectMatterIndexDto, int,
        PagedResultRequestDto, CreateSubjectMatterDto, UpdateSubjectMatterDto>
    {
        PagedResultDto<PrincipleDto> GetPrinciples(GetPrinciplesInput input);
    }
}
