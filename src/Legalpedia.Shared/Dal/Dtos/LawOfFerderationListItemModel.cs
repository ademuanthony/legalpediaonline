using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.DAL.Dtos
{
    public class LawOfFerderationListItemModel
    {

        public int Id { get; set; }

        public int? CatId { get; set; }

        public string LawNo { get; set; }

        public string Title { get; set; }

        public DateTime LawDate { get; set; }

        public string DateString => LawDate.ToLongDateString();

    }
}
