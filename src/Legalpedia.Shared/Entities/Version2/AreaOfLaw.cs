using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    [Table("AreasOfLaw")]
    public class AreaOfLaws
    {
        [Key]
        [Column("AreaOfLawID")]
        public int Id { get; set; }

        [Column("AreaOfLaw")]
        public string Title { get; set; }
    }
}
