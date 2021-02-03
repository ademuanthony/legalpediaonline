using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Judgements.Dto
{

   [AutoMapTo(typeof(Judgement))]
    public class UpdateJudgementDto:EntityDto<string>
    {
        public string Body { get; set; }
        
    }
}
