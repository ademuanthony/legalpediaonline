using Abp;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Notes.Dto
{
    [AutoMapFrom(typeof(Note))]
    public class NoteDto : Note
    {
        public bool Favourite { get; set; }
    }
}