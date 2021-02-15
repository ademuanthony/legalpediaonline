using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class JudgementPrinciples
    {
        [Key]
        [Column("JudgPrincId")]
        public int Id { get; set; }

        public int PrincipleId { get; set; }

        public string SuitNo { get; set; }

        [ForeignKey("SuitNo")]
        public JudgementsSummaries Summary { get; set; }

        [ForeignKey("PrincipleId")]
        public Principles Principle { get; set; }
        
    }
}
