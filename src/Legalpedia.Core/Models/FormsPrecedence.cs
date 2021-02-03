using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("forms_precedence")]
    public class FormsPrecedence:Entity
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
        [Column("version_no")]
        public int? VersionNo { get; set; }
    }
}
