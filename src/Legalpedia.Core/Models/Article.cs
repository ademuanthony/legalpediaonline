using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("articles")]
    public class Article:Entity
    {
        [Key]
        [Column("uuid")]
        public string Uuid { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("version_no")]
        public int? VersionNo { get; set; }
    }
}
