using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    public class Tags
    {
        [Column("TagId")]
        public int Id { get; set; }

        public string Tag { get; set; }

        [Timestamp]
        public byte[] LastUpdated { get; set; }
    }
}
