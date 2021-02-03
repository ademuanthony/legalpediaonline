using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("Tags")]
    public class Tag:Entity
    {
        [Column("TagId")]
        public override int Id { get; set; }

        [Column("Tag")]
        public string Text { get; set; }

    }
}
