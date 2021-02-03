using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.Models
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
    }
}
