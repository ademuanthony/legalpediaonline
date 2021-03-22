using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.FormsAndPrecedence.Dtos;
using System.Collections.Generic;

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
