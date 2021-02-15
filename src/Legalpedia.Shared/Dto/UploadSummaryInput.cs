using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legalpedia.DAL.Dtos;
using Legalpedia.DAL.Entities.Version2;

namespace Legalpedia.Shared.Dto
{
    public class UploadSummaryInput
    {
        public JudgementsSummaries Summary { get; set; }
        public List<string> Tags { get; set; }
        public List<string> CasesCited { get; set; }
        public List<LawOfFederation> StatusCited { get; set; }
        public List<Corams> Corams { get; set; }
        public List<string> PartyTypeANames { get; set; }
        public List<string> PartyTypeBNames { get; set; }
        public List<SummaryRatios> Ration { get; set; }
        public int PrincipleId { get; set; }
        public List<string> Counsels { get; set; }
        public List<string> HoldenAts { get; set; }
        public List<AreaOfLaws> AreasOfLaws { get; set; }
        public string JudgementBody { get; set; }
    }

    public class UploadResultDto
    {

    }
}
