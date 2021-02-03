using System.Collections.Generic;

namespace Legalpedia.Dto.DataSynchronisation
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
