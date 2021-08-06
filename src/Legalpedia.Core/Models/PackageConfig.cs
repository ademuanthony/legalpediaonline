using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class PackageConfig:Entity
    {
        public ResourceIdLabel ResourceIdLabel { get; set; }
        public string ResourceIdValue { get; set; }
        public int PackageId { get; set; }
        
        [ForeignKey("PackageId")]
        public Package Package { get; set; }
    }

    public enum ResourceIdLabel
    {
        
    }
}