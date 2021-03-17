using System;
using Abp.Application.Services.Dto;
using Legalpedia.Models;

namespace Legalpedia.Notes.Dto
{
    public class UpdateNoteDto: EntityDto<string>
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string ImageContent { get; set; }
        public NoteAccessType AccessType { get; set; }
        public string Labels { get; set; }
    }
}