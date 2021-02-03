using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class Coram:Entity
    {
        //[Key]
        //[Column(name: "CoramId")]
        //public override int Id { get; set; }

        public string Name { get; set; }

        public string EmailAddr { get; set; }


        public string PhoneNo { get; set; }

        public DateTime? Version { get; set; }

    } 
}
