using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("JudgementPartiesB")]
    public class JudgementPartiesB:Entity<string>
    {
        //[Key]
        //[Column("SuitNo")]
        //public override string Id { get; set; }

        public string PartyBNames { get; set; }

    }
}
