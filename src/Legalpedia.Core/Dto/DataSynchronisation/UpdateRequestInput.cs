using System;

namespace Legalpedia.Dto.DataSynchronisation
{
    public class UpdateRequestInput
    { 
        public DateTime Version { get; set; }
        public int MaxCount { get; set; }
        public int SkipCount { get; set; }
    }
}
