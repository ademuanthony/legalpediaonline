using Abp.Application.Services.Dto;

namespace Legalpedia.Notes.Dto
{
    public class NotesRequest: PagedResultRequestDto
    {
        public string SearchTerm { get; set; }
    }

    public class TeamNotesRequest : NotesRequest
    {
        public string TeamId { get; set; }
    }
}