using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    public class SbjMatterIndex
    {
        [Key]
        [Column("SbjMatterIdxId")]
        public int Id { get; set; }

        public string SubjectMatterIndex { get; set; }

        public override string ToString()
        {
            return SubjectMatterIndex;
        }
    }
}
