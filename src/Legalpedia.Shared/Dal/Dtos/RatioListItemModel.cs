using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Dtos
{
    public class RatioListItemModel
    {
        public string SuitNumber { get; set; }
        public string Heading { get; set; }
        public string Body { get; set; }
        public string CaseTitle { get; set; }
        public string Court { get; set; }
        public string Date { get; set; }
    }
}
