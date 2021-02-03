using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Maxims.Dtos
{
    public class GetAllMaximInput : PagedResultRequestDto
    {
        public string Title { get; set; }
    }
}
