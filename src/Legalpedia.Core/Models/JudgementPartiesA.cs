using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("JudgementPartiesA")]
    public class JudgementPartiesA:Entity<string>
    {
        //[Key]
        //[Column("SuitNo")]
        //public override string Id { get; set; } 

        public string PartyANames { get; set; }

    }
}
