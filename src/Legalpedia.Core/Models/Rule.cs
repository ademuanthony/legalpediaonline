using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("Rules")]
    public class Rule:Entity
    {
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
        [Column("version_no")]
        public int? VersionNo { get; set; }
    }
}
