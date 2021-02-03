using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Rules.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Rules
{
    public interface IRulesOfCourtAppService: IApplicationService
    {
        List<string> GetNames(string ruleType);
        bool Create(RuleDto rule);
        
        PagedResultDto<RuleDto> GetAll(GetAllRulesInput input);
        PagedResultDto<RuleDto> State(GetAllRulesInput input);
        PagedResultDto<RuleDto> Other(GetAllRulesInput input);

        RuleDto Detail(EntityDto<string> input);
    }
}
