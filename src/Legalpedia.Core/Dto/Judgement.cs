using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Legalpedia.Dto
{
    public class JudgementListItem
    {
        public string SuitNo { get; set; }
        public string CaseTitle { get; set; }
        public string Court { get; set; }
        public DateTime? JudgementDate { get; set; }
    }

    public class GetJudgementRequest : PagedResultRequestDto
    {
        public const string QueryNoSummary = "nosum";
        public string Query { get; set; }
    }
}
