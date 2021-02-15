using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Shared.Dto.DataSynchronisation
{
    public class UpdateCheck
    {
        public UpdateCheck() { }
        public UpdateCheck(string key, int value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; set; }
        public int Value { get; set; }
    }
    public class CheckUpdateOutput:List<UpdateCheck>
    {
    }
}
