using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("Categories")]
    public class Category:Entity
    {
        [Key] 
        [Column("CategoryId")]
        public override int Id { get; set; }

        [Column("Category")]
        public string Name { get; set; }
    }
}
