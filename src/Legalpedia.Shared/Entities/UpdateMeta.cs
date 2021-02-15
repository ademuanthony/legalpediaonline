using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities
{
    /**
     *
     create table UpdateMetas(
	    Date datetime,
	    DownloadLink nvarchar(250),
	    Description nvarchar(250),
	    Size int
     );
     *
     */
    [Table("UpdateMetas")]
    public class UpdateMeta
    {
        [Key]
        [Column("rowid")]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string DownloadLink { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
    }
}
