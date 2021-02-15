using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Legalpedia.Summaries.Dtos
{
    public class SummaryRatioDto:EntityDto<string>
    {
        public string Heading { get; set; }

        public string Coram { get; set; }

        public string Body { get; set; }

        public string SuitNo { get; set; }

        public JudgementSumaryDto Summary { get; set; }
    }
}
