using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Models;
using Legalpedia.Summaries.Dtos;
using Legalpedia.Dto;
using UploadResultDto = Legalpedia.Shared.Dto.UploadResultDto;
using UploadSummaryInput = Legalpedia.Summaries.Dtos.UploadSummaryInput;

namespace Legalpedia.Summaries
{
    /// <inheritdoc />
    public interface ISummariesAppService:IApplicationService
    {
        JudgementSummaryDto GetForEdit(EntityDto<string> input);
        UploadResultDto Post(UploadSummaryInput input);
        UploadResultDto Put(UploadSummaryInput input);
        Task<PagedResultDto<JudgementListItem>> GetListItems(GetJudgementRequest input);
        PagedResultDto<JudgementSummaryViewModel> Latest();
        PagedResultDto<JudgementSummaryViewModel> GetAll(GetAllSummaryInput input);
        JudgementSummaryViewModel Details(EntityDto<string> input);
        JudgementSummaryViewModel SummaryToViewModel(JudgementsSummary summary);
        bool UpdateCitation(int start, int end);
        string CitationUpdateSql();
        string RatioBodyById(int ratioId);
        string RationBodyByPrincipal(int principalId);
        string RatioBodyByTitle(string title);
    }
}
