using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;

namespace Legalpedia.Models
{
    public class Package:AuditedEntity
    {
        public const string MobilePlatform = "Mobile";
        public const string DesktopPlatform = "Desktop";
        public const string WebPlatform = "Web";

        public static List<string> PlatformCombinations = new List<string>
        {
            $"{WebPlatform}/{MobilePlatform}", $"{DesktopPlatform}/{MobilePlatform}", $"{WebPlatform}/{DesktopPlatform}/{MobilePlatform}"
        };
        public string Name { get; set; }
        public string Description { get; set; }
        public string Permalink { get; set; }
        /// <summary>
        /// Pipe seperated list of features for this package
        /// </summary>
        public string Features { get; set; }
        /// <summary>
        /// Slash seperated list of support platform
        /// </summary>
        public string Platforms { get; set; }

        public double Price { get; set; }
        public int Days { get; set; }
        
        public ICollection<PackageConfig> PackageConfigs { get; set; }
    }
}
