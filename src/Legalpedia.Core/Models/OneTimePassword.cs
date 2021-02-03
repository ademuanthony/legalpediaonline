using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Legalpedia.Models
{
    public class OneTimePassword:Entity, IHasCreationTime, IMayHaveTenant
    {
        public string Code { get; set; }
        public int CustomerId { get; set; }
        public string SystemId { get; set; }
        public long LicenseId { get; set; }
        public DateTime CreationTime { get; set; }

        public bool HasExpired()
        {
            return CreationTime.AddMinutes(15) < DateTime.Now;
        }

        public Customer Customer { get; set; }
        public int? TenantId { get; set; }
    }
}