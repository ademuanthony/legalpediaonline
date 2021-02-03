using Abp.AutoMapper;
using Abp.Domain.Entities;
using Legalpedia.Models;

namespace Legalpedia.SudjectMatters.Dto
{
    [AutoMapTo(typeof(SbjMatterIndex))]
    public class CreateSubjectMatterDto:Entity
    {
        public string SubjectMatterIndex { get; set; }
    }
}
