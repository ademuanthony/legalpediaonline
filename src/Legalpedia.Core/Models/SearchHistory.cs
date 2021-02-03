using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Legalpedia.Models
{
    public class SearchHistory: AuditedEntity
    {
        public string SearchWord { get; set; }
    }
}
