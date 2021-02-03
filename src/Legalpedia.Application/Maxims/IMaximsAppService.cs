using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Maxims.Dtos;

namespace Legalpedia.Maxims
{
    public interface IMaximsAppService: IApplicationService
    {
        PagedResultDto<MaximDto> GetAll(GetAllMaximInput input);
    }
}
