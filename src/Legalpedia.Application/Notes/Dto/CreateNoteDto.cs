using System;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Notes.Dto
{
    [AutoMapTo(typeof(Note))]
    public class CreateNoteDto
    {
        public string TeamId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string ImageContent { get; set; }
        public NoteAccessType AccessType { get; set; }
        public string Labels { get; set; }
    }
}