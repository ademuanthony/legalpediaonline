using Abp.Application.Services.Dto;

namespace Legalpedia.Summaries.Dtos
{
    public class SumAreasOfLawDto:EntityDto<string>
    {
        public string SuitNo { get; set; }

        public int AreasOfLawId { get; set; }

        public byte[] LastUpdated { get; set; }
    }
}
