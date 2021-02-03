using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class PartyAType:Entity
    {
        [Column("PartyAType")]
        public string Name { get; set; }
    }
}
