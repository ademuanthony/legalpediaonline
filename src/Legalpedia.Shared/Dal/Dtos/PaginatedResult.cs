using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Dtos
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(T data, int totalCount, int pageSize, int currentPage)
        {
            Data = data;
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
        }

        public PaginatedResult() { }

        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public int PageCount => (TotalCount % PageSize == 0
                                    ? TotalCount / PageSize
                                    : 1 + (TotalCount - TotalCount % PageSize) / PageSize);

        public bool IsFirstPage => CurrentPage == 1;

        public bool IsLastPage => CurrentPage == PageCount;

        public T Data { get; set; }

    }
}
