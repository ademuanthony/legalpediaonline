using System;
using System.Collections.Generic;
using Legalpedia.Models;

namespace Legalpedia.Summaries.Dtos
{
    public class JudgementSummaryViewModel
    {
        public string SuitNo { get; set; }

        public string Title { get; set; }

        public string SummaryOfFacts { get; set; }

        public string Held { get; set; }

        public string Issues { get; set; }

        public string CasesCited { get; set; }

        public string StatusCited { get; set; }

        public string LegalpediaCitation { get; set; }

        public DateTime? JudgementDate { get; set; }

        public string OtherCitations { get; set; }

        public string Court { get; set; }

        public string Category { get; set; }

        public List<string> Corams { get; set; }

        public List<string> AreasOfLaw { get; set; }

        public string Counsels { get; internal set; }

        public string HoldenAt { get; internal set; }

        public string PartyAType { get; set; }

        public string PartyBType { get; set; }

        public string PartiesA { get; internal set; }

        public string PartiesB { get; internal set; }

        public List<SummaryRatioDto> Ratios { get; set; }

        // the body is populated only for the details endpoint
        public string JudgementBody { get; set; }
        public List<JudgementPage> Pages { get; set; }
    }
}
