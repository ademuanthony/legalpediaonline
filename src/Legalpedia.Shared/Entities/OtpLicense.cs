using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("otp_license")]
    public class OtpLicense
    {
        [Key]
        [Column("rowid")]
        public int Id { get; set; }
        [Column("token")]
        public string Key { get; set; }

        [Column("live_update")]
        public DateTime LiveUpdate { get; set; }

        [Column("cur_date")]
        public DateTime CurDate { get; set; }

        //[Column("type")]
        //public string Type { get; set; }
    }
}
