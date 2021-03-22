using Abp.Application.Services.Dto;

namespace Legalpedia.Summaries.Dtos
{
    public class GetAllSummaryInput: PagedResultRequestDto
    { 
        public string Title { get; set; }
        public string TitlePrefix { get; set; }
        public string LpCitation { get; set; }
        public int CourtId { get; set; }
        public int SubjectMatterId { get; set; }
        public int PrincipleId { get; set; }
        public int Year { get; set; }
    }
}
