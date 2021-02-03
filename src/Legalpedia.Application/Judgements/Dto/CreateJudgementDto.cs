using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Judgements.Dto
{
    [AutoMapTo(typeof(Judgement))]
    public class CreateJudgementDto
    {
        public string Id { get; set; }

        public string Body { get; set; }
        
    }
}
