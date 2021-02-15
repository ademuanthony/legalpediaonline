using Abp.Application.Services.Dto;
using Legalpedia.Corams.Dto;

namespace Legalpedia.Summaries.Dtos
{
    public class JudgementCoramDto:EntityDto<string>
    {
        public int CoramId { get; set; }

        public string JudgementId { get; set; }
         

        public CoramDto Coram { get; set; }
    }
}
