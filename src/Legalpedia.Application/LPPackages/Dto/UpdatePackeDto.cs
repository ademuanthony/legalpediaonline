﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Packages.Dto
{
    [AutoMapFrom(typeof(Package))]
    [AutoMapTo(typeof(Package))]
    public class UpdatePackageDto: AuditedEntityDto
    {
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
    }
}
