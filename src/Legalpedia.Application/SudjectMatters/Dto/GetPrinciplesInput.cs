using Abp.Application.Services.Dto;

namespace Legalpedia.SudjectMatters.Dto
{
    public class GetPrinciplesInput: PagedResultRequestDto
    {
        public int? SubjectMatterId { get; set; }
    }
}
