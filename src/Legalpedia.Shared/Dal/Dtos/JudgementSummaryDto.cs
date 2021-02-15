using System;
using System.Collections.Generic;
using Legalpedia.DAL.Entities.Version2;

namespace Legalpedia.DAL.Dtos
{
    [Serializable]
    public class JudgementSummaryDto
    {
        public JudgementsSummaries Summary { get; set; }

        public PartyATypes PartyAType { get; set; }

        public PartyBTypes PartyBType { get; set; }

        public Categories Category { get; set; }

        public HoldenAts HoldenAt { get; set; }

        public JudgementCounsels Counsel { get; set; }

        public JudgementPartiesA PartiesA { get; set; }

        public JudgementPartiesB PartiesB { get; set; }

        public List<JudgementPrinciples> Principles { get; set; }

        public List<SummaryRatios> Ration { get; set; }

        public List<Corams> Corams { get; set; }

        public List<AreaOfLaws> AreasOfLaws { get; set; }

        public string JudgementBody { get; set; }
    }
}
