using Abp.Application.Services.Dto;

namespace Legalpedia.Dictionaries.Dtos
{
    public class DictionaryDto: EntityDto
    {
        public string Uuid { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int? VersionNo { get; set; }
    }
}
