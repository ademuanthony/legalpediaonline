using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Judgements.Dto
{
   [AutoMapFrom(typeof(Judgement))]
    public class JudgementDto:EntityDto<string>
    {
        public string Body { get; set; }
        
    }
}
