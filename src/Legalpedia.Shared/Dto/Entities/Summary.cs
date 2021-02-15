using System;

namespace Legalpedia.Shared.Dto.Entities
{
    public class Summary
    {
        public long Id { get; set; }
        public string Uuid { get; set; }
        public string CaseTitle { get; set; }
        public string AreaOfLaw { get; set; }
        public string Courts { get; set; }
        public string Counsel { get; set; }
        public string SummaryOfFacts { get; set; }
        public string Held { get; set; }
        public string Issue { get; set; }
        public string Ratio { get; set; }
        public string CategoryTags { get; set; }
        public string CasesCitied { get; set; }
        public string StatusCited { get; set; }
        public string SubjectMatter { get; set; }
        public string PrincipalUuid { get; set; }
        public string CaseId { get; set; }
        public string CaseUuid { get; set; }
        public string JudgementDate { get; set; }
        public string ICitation { get; set; }
        public string OCitations { get; set; }
        public string SittingAt { get; set; }
        public string SuitNumber { get; set; }
        public string Coram { get; set; }
        public string PartyAType { get; set; }
        public string PartyAName { get; set; }
        public string PartyBType { get; set; }
        public string PartyBName { get; set; }
        public DateTime Date { get; set; }
        public string JudgeMonth { get; set; }
        public string DatePosted { get; set; }
        public string DateUpdated { get; set; }
    }
}
