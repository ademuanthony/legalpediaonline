using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Serializable]
    [Table("rules")]
    public class Rule
    {
        [Key]
        [Column("uuid")]
        public string Uuid { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("section")]
        public string Section { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("content")]
        public string Content { get; set; }
    }
}
