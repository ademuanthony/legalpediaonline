using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    public class JudgementCorams
    {
        [Column("JudgmentCoramID")]
        [Key]
        public int Id { get; set; }

        public int CoramId
        {
            get; set; 
        }

        [Column("SuitNo")]
        public string JudgementId { get; set; }

        [ForeignKey("CoramId")]
        public Corams Coram { get; set; }
    }
}
