using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Packages.Dto
{
    [AutoMapFrom(typeof(Package))]
    public class PackageDto: AuditedEntityDto
    {
        public string Name { get; set; }
        /// <summary>
        /// Permalink gives the link for accessing this package on the web. Products withouth permalink are not
        /// accessible
        /// </summary>
        public string Permalink { get; set; }
        /// <summary>
        /// Pipe seperated list of features
        /// </summary>
        public string Features { get; set; }
        /// <summary>
        /// Slash seperated list of support platform
        /// </summary>
        public string Platforms { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
        public int Days { get; set; }
    }
}
