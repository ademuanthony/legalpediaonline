using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("Edited_All_UUID")]
    public class EditedAllUuid
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("VERSION_NO")]
        public long VersionNo { get; set; }
        [Column("TYPE")]
        public string Type { get; set; }
    }
}
