using Abp.Domain.Repositories;
using Legalpedia.Models;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Maxims.Dtos;

namespace Legalpedia.Dictionaries
{
    public class DictionaryAppService: LegalpediaAppServiceBase, IDictionaryAppService
    {
        IRepository<Dictionary> _repository;
        public DictionaryAppService(IRepository<Dictionary> repository)
        {
            _repository = repository;
        }

        public PagedResultDto<MaximDto> GetAll(GetAllMaximInput input)
        {
            List<Dictionary> records;
            int count;

            if (string.IsNullOrEmpty(input.Title))
            {
                records = _repository.GetAll().OrderBy(d => d.Title).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                count = _repository.Count();
            }
            else
            {
                records = _repository.GetAll().Where(d => d.Title.Contains(input.Title)).OrderBy(f => f.Title).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                count = _repository.Count(d => d.Title.Contains(input.Title));
            }

            return new PagedResultDto<MaximDto>(count, records.MapTo<List<MaximDto>>());
        }
    }
}
