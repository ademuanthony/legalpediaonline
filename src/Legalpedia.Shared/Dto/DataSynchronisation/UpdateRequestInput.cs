using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Shared.Dto.DataSynchronisation
{
    public class UpdateRequestInput
    { 
        public DateTime Version { get; set; }
        public int MaxCount { get; set; }
        public int SkipCount { get; set; }
    }
}
