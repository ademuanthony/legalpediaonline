using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class ForeignLegalResource: Entity
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}