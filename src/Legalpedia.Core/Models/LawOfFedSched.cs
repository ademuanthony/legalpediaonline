using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class LawOfFedSched:Entity
    {
        //[Key]
        //[Column("SchedId")]
        //public override int Id { get; set; }

        public string SchedHeader { get; set; }

        public string SchedBody { get; set; }

        public int LawId { get; set; }
    }
}
