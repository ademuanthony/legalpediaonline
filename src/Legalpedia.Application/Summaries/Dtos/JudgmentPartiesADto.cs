using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Legalpedia.Summaries.Dtos
{
    public class JudgmentPartiesADto:EntityDto<string>
    {
        public string PartyANames { get; set; }

        [Timestamp]
        public byte[] LastUpdated { get; set; }
    }
}
