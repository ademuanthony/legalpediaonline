using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("ALL_UUID")]
    public class AllUuid
    {
        [Key]
        public long Id { get; set; }
        [Column("all_uuid")]
        public string AllUuidRow { get; set; }
    }
}
