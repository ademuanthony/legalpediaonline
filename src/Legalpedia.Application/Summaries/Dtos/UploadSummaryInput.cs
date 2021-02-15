using System.Collections.Generic;
using Legalpedia.Corams.Dto;
using Legalpedia.Laws.LawsOfFederations.Dtos;
using Legalpedia.Models;

namespace Legalpedia.Summaries.Dtos
{
    public class UploadSummaryInput 
    {
        public JudgementSumaryDto Summary { get; set; }
        public List<string> Tags { get; set; }
        public List<string> CasesCited { get; set; }
        public List<LawOfFederationDto> StatusCited { get; set; }
        public List<CoramDto> Corams { get; set; }
        public List<string> PartyTypeANames { get; set; }
        public List<string> PartyTypeBNames { get; set; } 
        public List<SummaryRatioDto> Ration { get; set; }
        public int PrincipleId { get; set; }
        public List<string> Counsels { get; set; }
        public List<string> HoldenAts { get; set; }
        public List<AreaOfLaw> AreasOfLaws { get; set; }
        public string JudgementBody { get; set; }
    }
}
