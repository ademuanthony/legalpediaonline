using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.SudjectMatters.Dto
{
    [AutoMapFrom(typeof(SbjMatterIndex))]
    public class SubjectMatterIndexDto : EntityDto
    {
        public string SubjectMatterIndex { get; set; }
    }
}
