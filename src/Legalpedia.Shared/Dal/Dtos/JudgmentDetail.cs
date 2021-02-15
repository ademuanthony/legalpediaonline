using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legalpedia.DAL.Entities.Version2;

namespace Legalpedia.DAL.Dtos
{
    public class JudgmentDetail
    {
        public JudgementsSummaries Summary { get; set; }
        public Judgements Judgment { get; set; }
        public List<SummaryRatios> Ration { get; set; }
        public Courts Court { get; set; }
        public List<Corams> Corams { get; set; }

        // public AreaOfLaws AreaOfLaw { get; set; }
        public List<AreaOfLaws> AreasOfLaw { get; set; }
    }
}
