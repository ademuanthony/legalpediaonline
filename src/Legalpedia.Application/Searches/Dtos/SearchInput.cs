using Abp.Application.Services.Dto;
using Legalpedia.Models;

namespace Legalpedia.Searches.Dtos
{
    public class SearchInput : PagedResultRequestDto
    {
        public string Term { get; set; }
        
        public IndexType Type { get; set; }
    }
}