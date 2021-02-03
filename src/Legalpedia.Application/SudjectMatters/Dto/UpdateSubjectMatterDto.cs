using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.SudjectMatters.Dto
{
    [AutoMapTo(typeof(SbjMatterIndex))]
    public class UpdateSubjectMatterDto:EntityDto
    {
        public string SubjectMatterIndex { get; set; }
    }
}
