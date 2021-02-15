using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class JudgementCounsels
    {
        [Key]
        [Column("SUITNO")]
        public string Id { get; set; }

        public string Counsels { get; set; }
        
    }
}
