using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Legalpedia.Summaries.Dtos
{
    public class JudgmentPartiesBDto:EntityDto
    {
        public string PartyBNames { get; set; }

        [Timestamp]
        public byte[] LastUpdated { get; set; }
    }
}
