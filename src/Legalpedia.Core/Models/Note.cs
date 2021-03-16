using System;
using Abp.Domain.Entities;
using JetBrains.Annotations;

namespace Legalpedia.Models
{
    public class Note:Entity<string>
    {
        [CanBeNull] 
        public string TeamId { get; set; }
        public long CreatorId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public NoteAccessType AccessType { get; set; }
        public string Labels { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum NoteAccessType
    {
        Private = 0,
        Public = 1,
    }

    public class FavouriteNote:Entity<string>
    {
        public string NoteId { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class NoteComment: Entity<string>
    {
        public string NoteId { get; set; }
        public long CreatorId { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SharedNote: Entity<string>
    {
        public string NoteId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}