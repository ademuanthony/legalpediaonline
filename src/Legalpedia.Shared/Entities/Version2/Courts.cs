using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Serializable]
    public class Courts
    {
        public const string CourtOfAppeal = "In the Court of Appeal";

        public const string Tribunal = "In the Investments and Securities Tribunal";

        public const string NationalIndustrialCourt = "In the National Industrial Court of Nigeria";

        public const string SupremeCourt = "In the Supreme Court of Nigeria";

        public const string ShariaCourt = "In the Sharia Court";

        public const string FederalHighCourt = "In the Federal High Court";

        [Column("CourtID")]
        [Key]
        public int Id { get; set; }

        public string Court { get; set; }

        public int Superiority { get; set; }

        public override string ToString()
        {
            return Court;
        }
    }
}
