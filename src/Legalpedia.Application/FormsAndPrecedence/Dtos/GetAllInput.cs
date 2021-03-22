using Abp.Application.Services.Dto;

namespace Legalpedia.FormsAndPrecedence.Dtos
{
    public class GetAllFormsAndPrecedenceInput : PagedResultRequestDto
    {
        public string Category { get; set; }
        public string Title { get; set; }
    }
}
