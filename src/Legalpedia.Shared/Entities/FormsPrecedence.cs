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
    [Table("forms_precedence")]
    public class FormsPrecedence
    {
        [Key]
        [Column("uuid")]
        public string Uuid { get; set; }
        [Column("category")]
        public string Category { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("content")]
        public string Content { get; set; }
    }
}
