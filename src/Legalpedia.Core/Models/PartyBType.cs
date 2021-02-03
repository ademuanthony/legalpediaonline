using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class PartyBType:Entity
    {
        [Column("PartyBType")]
        public string Name { get; set; }
    }
}
