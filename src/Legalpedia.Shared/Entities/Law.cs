using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("laws")]
    public class Law
    {
        [Key]
        [Column("uuid")]
        public string Uuid { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("number")]
        public string Number { get; set; }
        [Column("date")]
        public string Date { get; set; }
        [Column("description")]
        public string Description { get; set; }
    }
}
