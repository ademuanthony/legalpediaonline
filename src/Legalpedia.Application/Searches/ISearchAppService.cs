using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Searches.Dtos;
using System.Threading.Tasks;

namespace Legalpedia.Searches
{
    public interface ISearchAppService:IApplicationService
    {
        PagedResultDto<KeywordDto> GetAll(PagedResultRequestDto input);
        Task<SearchResult> Search(SearchInput input);
    }
}
