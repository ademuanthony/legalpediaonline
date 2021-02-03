using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.SudjectMatters.Dto
{
    public class GetPrinciplesInput: PagedResultRequestDto
    {
        public int? SubjectMatterId { get; set; }
    }
}
