using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Laws.LawsOfFederations.Dtos
{
    public class FilterInput: PagedResultRequestDto
    {
        public string Title { get; set; }

        public int Year { get; set; }
    }
}
