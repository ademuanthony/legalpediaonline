using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Legalpedia.Models;
using Index = Legalpedia.Models.Index;

namespace Legalpedia.Searches.Dtos
{
    public class SearchResult : PagedResultDto<Index>
    {
        public SearchResult(int totalCount, IReadOnlyList<Index> items, TimeSpan duration):base(totalCount, items)
        {
            Duration = duration;
        }
        public TimeSpan Duration { get; set; }
    }
}