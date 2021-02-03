using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("UpdateMetas")]
    public class UpdateMeta:Entity
    {
        public DateTime Date { get; set; }
        public string DownloadLink { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
    }
}
