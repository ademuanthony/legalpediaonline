using Abp.Application.Services.Dto;

namespace Legalpedia.Dictionaries.Dtos
{
    public class GetAllDictionaryInput: PagedResultRequestDto
    {
        public string Title { get; set; }
    }
}
