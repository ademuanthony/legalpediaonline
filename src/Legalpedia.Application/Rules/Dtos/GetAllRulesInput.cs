using Abp.Application.Services.Dto;

namespace Legalpedia.Rules.Dtos
{
    public class GetAllRulesInput: PagedResultRequestDto
    {
        public string Title { get; set; }
         
        public string StateName { get; set; }
        public string Section { get; set; }
    }
}
