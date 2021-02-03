using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Legalpedia.Models
{
    [Table("License")]
    public class License : AuditedEntity, IMayHaveTenant
    {
        public int CustomerId { get; set; }
        public string SystemId { get; set; }
        public string MobileSystemId { get; set; }
        public int LicensedDays { get; set; }
        public int UpdateLicensedDays { get; set; }
        public DateTime? UnlockDate
        {
            get; set;
        }
        public string Package { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public DateTime? ExpDate
        {
            get;set;
        }


        public bool HasExpired()
        {
            return ExpDate <= DateTime.Now;
        }

        public int? TenantId { get; set; }
    }
}