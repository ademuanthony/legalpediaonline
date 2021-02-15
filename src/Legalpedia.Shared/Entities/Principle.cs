using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("principles")]
    public class Principle
    {
        [Key]
        [Column("uuid")]
        public string Uuid { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("subject")]
        public string Subject { get; set; }
    }
}
