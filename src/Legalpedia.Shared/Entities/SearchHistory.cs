using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Entities
{
    [Table("search_history")]
    public class SearchHistory
    {
        [Key]
        [Column("rowid")]
        public long Id { get; set; }
        [Column("uuid")]
        public string Uuid { get; set; }
        [Column("search_word")]
        public string SearchWord { get; set; }
        [Column("search_date")]
        public string SearchDate { get; set; }

        public override string ToString()
        {
            return SearchWord;
        }
    }
}
