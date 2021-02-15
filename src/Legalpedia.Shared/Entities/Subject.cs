using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("subjects")]
    public class Subject
    {
        [Column("uuid")]
        [Key]
        public string Uuid { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}
