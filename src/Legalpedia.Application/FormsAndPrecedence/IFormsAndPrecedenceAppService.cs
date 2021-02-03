using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.FormsAndPrecedence.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.FormsAndPrecedence
{
    public interface IFormsPrecedenceAppService : IApplicationService
    {
        FormsAndPrecedenceDto Create(FormsAndPrecedenceDto input);
        PagedResultDto<FormsAndPrecedenceDto> GetAll(GetAllFormsAndPrecedenceInput input);
        FormsAndPrecedenceDto Detail(EntityDto<string> input);
        List<string> GetCategories();
    }
}
