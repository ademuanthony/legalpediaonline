using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
