using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("JudgementCorams")]
    public class JudgementCoram:Entity
    {
        //[Column("JudgmentCoramID")]
        //[Key]
        //public override int Id { get; set; }

        public int CoramId
        {
            get; set; 
        }

        [Column("SuitNo")]
        public string JudgementId { get; set; }

        [ForeignKey("CoramId")]
        public Coram Coram { get; set; }

        //[ForeignKey("JudgementId")]
        //public JudgementsSummaries Summary { get; set; }
    }
}
