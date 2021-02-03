using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("JudgementCounsels")]
    public class JudgementCounsel:Entity<string>
    {
        //[Key]
        //[Column("SUITNO")]
        //public override string Id { get; set; }

        public string Counsels { get; set; }

        //[ForeignKey("Id")]
        //public JudgementsSummaries Summary { get; set; }
    }
}
