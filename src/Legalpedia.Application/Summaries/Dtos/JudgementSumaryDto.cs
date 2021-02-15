using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Legalpedia.Summaries.Dtos
{
    public class JudgementSumaryDto:EntityDto<string>
    {
        public string Title { get; set; }

        public string SummaryOfFacts { get; set; }

        public string Held { get; set; }

        public string Issues { get; set; }

        public string CasesCited { get; set; }

        public string StatusCited { get; set; }

        public string LpCitation { get; set; }

        public DateTime JudgementDate { get; set; }

        public string OtherCitations { get; set; }

        public int? HoldenAtId { get; set; }

        public int? CourtId { get; set; }

        public int? PartyATypeId { get; set; }

        public int? PartyBTypeId { get; set; }

        public int? CategoryId { get; set; }


        public ICollection<SummaryRatioDto> Ration { get; set; }
    }
}
