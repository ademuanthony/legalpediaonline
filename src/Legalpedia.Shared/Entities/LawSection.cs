using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("law_sections")]
    public class LawSection
    {
        [Key]
        [Column("uuid")]
        public string Uuid { get; set; }
        [Column("law_uuid")]
        public string LawUuid { get; set; }
        [Column("part_schedule")]
        public string PartScedule { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("body")]
        public string Body { get; set; }
    }
}
