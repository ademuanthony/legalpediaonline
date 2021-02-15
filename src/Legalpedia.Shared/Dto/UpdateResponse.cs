using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Shared.Dto
{
    public class UpdateResponse<TDate>
    {
        public DateTime MaxId { get; set; }
        public List<TDate> Items { get; set; }
    }
}
