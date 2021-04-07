using Abp.Application.Services.Dto;

namespace Legalpedia.Notes.Dto
{
    public class UpdateCommentInput:EntityDto<string>
    {
        public string Body { get; set; }
    }
}