using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class PartyATypes
    {
        [Key]
        [Column("PartyATypeId")]
        public int Id { get; set; }

        public string PartyAType { get; set; }

        public override string ToString()
        {
            return PartyAType;
        }
    }
}
