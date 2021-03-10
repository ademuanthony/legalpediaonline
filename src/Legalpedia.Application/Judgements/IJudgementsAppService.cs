using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Judgements.Dto;
using Legalpedia.Models;
using Legalpedia.Dto;

namespace Legalpedia.Judgements
{
    public interface IJudgementsAppService : IAsyncCrudAppService<JudgementDto, string,
        PagedResultRequestDto, CreateJudgementDto, UpdateJudgementDto>
    {
        JudgementDto Post(JudgementDto input);
        JudgementDto Put(JudgementDto input);
        PagedResultDto<JudgementListItem> GetJudgements(GetJudgementRequest input);
        List<JudgementPage> GetPages(EntityDto<string> entityDto);
    }
}
