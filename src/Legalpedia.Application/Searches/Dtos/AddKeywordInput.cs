using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Searches.Dtos
{
    public class AddKeywordInput
    {
        public string Text { get; set; }

        public int RatioCount { get; set; }

        public int SummaryCount { get; set; }

        public int ResultCount { get; set; }
    }
}
