using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legalpedia.DAL.Entities;

namespace Legalpedia.DAL.Dtos
{
    public class SearchResult
    {
        public long RatioCount { get; set; }
        public long SummaryCount { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
