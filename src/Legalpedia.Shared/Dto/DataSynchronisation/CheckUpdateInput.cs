using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Shared.Dto.DataSynchronisation
{
    public class UpdateType
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
    public class CheckUpdateInput
    {
        public DateTime LastUpdateAt { get; set; }
        public string ClientId { get; set; }
        public string Token { get; set; }
        public List<UpdateType> Items { get; set; }
    }
}
