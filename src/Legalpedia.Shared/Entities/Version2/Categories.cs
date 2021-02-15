using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class Categories
    {
        [Key]
        [Column("CategoryID")]
        public int Id { get; set; }

        public string Category { get; set; }

        public override string ToString()
        {
            return Category;
        }
    }
}
