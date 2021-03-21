using System;
using Abp.Application.Services.Dto;
using JetBrains.Annotations;
using Legalpedia.Models;

namespace Legalpedia.Notes.Dto
{
    public class NoteHeaderDto: EntityDto<string>
    {
        [CanBeNull] public string TeamId { get; set; }
        public long CreatorId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public NoteAccessType AccessType { get; set; }
        public string Labels { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public string CreatorName { get; set; }
        public string CreatorImage { get; set; }
    }
}