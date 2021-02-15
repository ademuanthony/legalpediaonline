using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Dtos
{
    public class PaginationInfo
    {
        public PaginationInfo(int page, int pageSize = 20, bool paginate = true)
        {
            Page = page;
            PageSize = pageSize;
            Paginate = paginate;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool Paginate { get; set; }

        public int Offset => (Page - 1) * PageSize;
    }
}
