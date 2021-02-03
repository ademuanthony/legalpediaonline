using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class LicensePackage:Entity
    {
        public string Name { get; set; }
        public int Days { get; set; }
        public bool IsAdmin { get; set; }
    }
}
