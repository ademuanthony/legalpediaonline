using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class Judgements
    {
        public Judgements()
        {
            CreatedAt = DateTime.Now;
        }
        [Key]
        [ Column("SuitNo")]
        public string Id { get; set; }

        [Column("Judgement")]
        public string Body { get; set; }

        public DateTime? CreatedAt { get; set; }


    }
}
