using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Maxims.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Dictionaries
{
    public interface IDictionaryAppService: IApplicationService
    {
        PagedResultDto<MaximDto> GetAll(GetAllMaximInput input);
    }
}
