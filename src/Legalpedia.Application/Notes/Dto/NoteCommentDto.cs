using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Notes.Dto
{
    [AutoMapFrom(typeof(NoteComment))]
    public class NoteCommentDto: NoteComment
    {
        public string CreatorName { get; set; }
        public bool AllowEdit { get; set; }
    }
}