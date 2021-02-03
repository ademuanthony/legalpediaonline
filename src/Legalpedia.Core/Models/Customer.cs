using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Legalpedia.Models
{
    public class Customer:AuditedEntity,IMayHaveTenant
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }


        public ICollection<License> Licenses { get; set; }
        public int? TenantId { get; set; }
    }
}