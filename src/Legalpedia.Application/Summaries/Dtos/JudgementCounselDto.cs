using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

//using Legalbackend.Legalpedia.Version2;

namespace Legalpedia.Summaries.Dtos
{
    [AutoMapFrom(typeof(JudgementCounsel))]
    public class JudgementCounselDto:EntityDto
    {
        public string Counsels { get; set; }

    }
}
