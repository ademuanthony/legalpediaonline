using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.FormsAndPrecedence.Dtos
{
    [AutoMapFrom(typeof(FormsPrecedence))]
    public class FormsAndPrecedenceDto:EntityDto
    {
        public string Uuid { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
