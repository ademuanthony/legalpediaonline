using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    public class Court:Entity 
    {
        public const string CourtOfAppeal = "In the Court of Appeal";

        public const string Tribunal = "In the Investments and Securities Tribunal";

        public const string NationalIndustrialCourt = "In the National Industrial Court of Nigeria";

        public const string SupremeCourt = "In the Supreme Court of Nigeria";

        public const string ShariaCourt = "In the Sharia Court";

        public const string FederalHighCourt = "In the Federal High Court";

        [Column("Court")]
        public string Name { get; set; }
    }
}
