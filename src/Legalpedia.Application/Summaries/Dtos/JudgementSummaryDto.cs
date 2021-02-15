using System;
using System.Collections.Generic;
using Legalpedia.Models;
using Legalpedia.Parties.PartATypes.Dto;
using JudgementPartiesA = Legalpedia.Models.JudgementPartiesA;
using JudgementPartiesB = Legalpedia.Models.JudgementPartiesB;

namespace Legalpedia.Summaries.Dtos
{
    public class JudgementSummaryDto
    {
        public JudgementsSummary Summary { get; set; }

        public PartyATypeDto PartyAType { get; set; }

        public PartyBType PartyBType { get; set; }

        public Category Category { get; set; }

        public HoldenAt HoldenAt { get; set; }

        public JudgementCounsel Counsel { get; set; }

        public JudgementPartiesA PartiesA { get; set; }

        public JudgementPartiesB PartiesB { get; set; }

        public ICollection<JudgementPrinciple> Principles { get; set; }

        public ICollection<SummaryRatio> Ration { get; set; }

        public ICollection<Coram> Corams { get; set; }

        public List<AreaOfLaw> AreasOfLaws { get; set; }
    }
}
