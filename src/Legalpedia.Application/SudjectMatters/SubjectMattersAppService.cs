using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Legalpedia.Models;
using Legalpedia.Principles.Dto;
using Legalpedia.SudjectMatters.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Legalpedia.SudjectMatters
{
    public class SubjectMattersAppService : AsyncCrudAppService<SbjMatterIndex,
        SubjectMatterIndexDto, int, PagedResultRequestDto, CreateSubjectMatterDto, UpdateSubjectMatterDto>,
        ISubjectMattersAppService
    {
        IRepository<Principle> _principleRepository;

        public SubjectMattersAppService(IRepository<SbjMatterIndex> repository, IRepository<Principle> principleRepository) :
            base(repository)
        {
            _principleRepository = principleRepository;
        }

        public PagedResultDto<PrincipleDto> GetPrinciples(GetPrinciplesInput input)
        {
            var query = _principleRepository.GetAll().Where(p => p.SbjMatterIdxId == input.SubjectMatterId);
            var records = query.OrderBy(p => p.Name).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var totalCount = query.Count();
            return new PagedResultDto<PrincipleDto>(totalCount, records.MapTo<List<PrincipleDto>>());
        }
    }
}
