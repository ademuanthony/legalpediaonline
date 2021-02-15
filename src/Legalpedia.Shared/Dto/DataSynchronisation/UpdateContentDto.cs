using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Shared.Dto.DataSynchronisation
{
    public class UpdateContentDto<T>
    {
        public bool IsNewRecord { get; set; }
        public T Content { get; set; }
    }
}
