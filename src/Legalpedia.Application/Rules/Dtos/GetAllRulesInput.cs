using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Rules.Dtos
{
    public class GetAllRulesInput: PagedResultRequestDto
    {
        public string Title { get; set; }
         
        public string StateName { get; set; }
        public string Section { get; set; }
    }
}
