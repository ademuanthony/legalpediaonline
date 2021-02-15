using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class Corams
    {
        [Key]
        [Column(name: "CoramId")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string EmailAddr { get; set; }


        public string PhoneNo { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
