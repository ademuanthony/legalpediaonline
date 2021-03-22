using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Maxims.Dtos;

namespace Legalpedia.Dictionaries
{
    public interface IDictionaryAppService: IApplicationService
    {
        PagedResultDto<MaximDto> GetAll(GetAllMaximInput input);
    }
}
