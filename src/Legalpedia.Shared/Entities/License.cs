using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("License2")]
    public class License
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("LICENSE_ID")]
        public string LicenseId { get; set; }
        [Column("SYSTEM_ID")]
        public string SystemId { get; set; }
        [Column("PREUNLOCK")]
        public string PreUnlock { get; set; }
        [Column("UNLOCK")]
        public string Unlock { get; set; }
        [Column("LICENSE")]
        public long LicensedDays { get; set; }
        [Column("UNLOCK_DATE")]
        public DateTime UnlockDate { get; set; }
        [Column("EXP_DATE")]
        public string ExpDate { get; set; }
        [Column("CUR_DATE")]
        public DateTime CurDate { get; set; }
        [Column("UPDATE_DATE")]
        public DateTime UpdateDate { get; set; }
        [Column("END_UPDATE")]
        public string EndUpdate { get; set; }
        [Column("LIVE_UPDATE")]
        public string LiveUpdate { get; set; }
        [Column("CUSTOMER_NAME")]
        public string CustomerName { get; set; }
        [Column("ADMIN_CODE")]
        public string AdminCode { get; set; }
        [Column("PACKAGE")]
        public string Package { get; set; }
        [Column("BIRTH_DAY")]
        public string BirthDay { get; set; }
        [Column("BIRTH_MONTH")]
        public string BirthMonth { get; set; }
    }
}
