using Abp.Application.Services.Dto;

namespace Legalpedia.Laws.LawsOfFederations.Dtos
{
    public class FilterInput: PagedResultRequestDto
    {
        public string Title { get; set; }

        public int Year { get; set; }
    }
}
