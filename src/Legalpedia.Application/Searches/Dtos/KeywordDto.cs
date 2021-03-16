using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Searches.Dtos
{
    [AutoMapTo(typeof(Keyword))]
    [AutoMapFrom(typeof(Keyword))]
    public class KeywordDto: EntityDto
    {
        public string Text { get; set; }

        public int RatioCount { get; set; }

        public int SummaryCount { get; set; }

        public int ResultCount { get; set; }

        public DateTime LastIndexingDate { get; set; }

        public DateTime Version { get; set; }
    }
}
