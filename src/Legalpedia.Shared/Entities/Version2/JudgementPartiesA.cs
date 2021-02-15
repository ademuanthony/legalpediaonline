using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    [Table("JudgementPartiesA")]
    public class JudgementPartiesA
    {
        [Key]
        [Column("SuitNo")]
        public string Id { get; set; } 

        public string PartyANames { get; set; }
        
    }
}
