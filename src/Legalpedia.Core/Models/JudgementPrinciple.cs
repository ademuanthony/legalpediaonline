using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("JudgementPrinciples")]
    public class JudgementPrinciple:Entity
    {
        //[Key]
        //[Column("JudgPrincId")]
        //public override int Id { get; set; }

        public int PrincipleId { get; set; }

        public string SuitNo { get; set; }
    }
}
