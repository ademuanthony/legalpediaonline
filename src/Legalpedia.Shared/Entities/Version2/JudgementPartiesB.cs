using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    [Table("JudgementPartiesB")]
    public class JudgementPartiesB
    {
        [Key]
        [Column("SuitNo")]
        public string Id { get; set; }

        public string PartyBNames { get; set; }
        
    }
}
