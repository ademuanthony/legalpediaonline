using System;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class BookmarkCollection: Entity<string>
    {
        public string Name { get; set; }
        public long UserId { get; set; }
    }
}