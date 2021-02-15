using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class PartyBTypes
    {
        [Key]
        [Column("PartyBTypeId")]
        public int Id { get; set; }

        public string PartyBType { get; set; }

        public override string ToString()
        {
            return PartyBType;
        }
    }
}
