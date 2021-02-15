using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("UPDATED_ID")]
    public class UpdatedId
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("file_id")]
        public long FileId { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("uuid")]
        public string Uuid { get; set; }

        [Column("version")]
        public DateTime? Version { get; set; }
    }
}
