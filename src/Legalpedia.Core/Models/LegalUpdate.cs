using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.Models
{
    [Table("legal_update")]
    public class LegalUpdate
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("DOCID")]
        public long DocId { get; set; }
        [Column("DOCNAME")]
        public string DocName { get; set; }
        [Column("DOCTYPE")]
        public string DocType { get; set; }
        [Column("DOCDATE")]
        public string DocDate { get; set; }
        [Column("VERSION_NO")]
        public long? VersionNo { get; set; }
        [Column("DATE")]
        public DateTime Date { get; set; }
    }
}
