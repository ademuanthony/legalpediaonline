using Abp.Application.Services.Dto;

namespace Legalpedia.Maxims.Dtos
{
    public class GetAllMaximInput : PagedResultRequestDto
    {
        public string Title { get; set; }
    }
}
