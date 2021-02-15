using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class Principles
    {
        [Key]
        [Column("PrincipleId")]
        public int Id { get; set; }

        public string Principle { get; set; }

        public int? SbjMatterIdxId { get; set; }

        public override string ToString()
        {
            return Principle;
        }
    }
}
