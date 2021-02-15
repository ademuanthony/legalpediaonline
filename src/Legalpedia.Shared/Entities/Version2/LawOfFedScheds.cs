using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class LawOfFedScheds
    {
        [Key]
        [Column("SchedId")]
        public int Id { get; set; }

        public string SchedHeader { get; set; }

        public string SchedBody { get; set; }

        public int LawId { get; set; }
        
    }
}
