using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    public class SumAreasOfLaw
    {
        [Key]
        [Column("SumAreaOfLawID")]
        public int Id { get; set; }

        public string SuitNo { get; set; }

        public int AreaOfLawId { get; set; }

        [ForeignKey("AreaOfLawId")]
        public AreaOfLaws AreaOfLaw { get; set; }
        
        [ForeignKey("SuitNo")]
        public JudgementsSummaries Summary { get; set; }
    }
}
