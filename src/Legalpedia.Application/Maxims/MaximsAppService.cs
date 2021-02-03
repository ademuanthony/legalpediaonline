using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Legalpedia.Models;
using Legalpedia.Maxims.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Legalpedia.Maxims
{
    public class MaximsAppService: LegalpediaAppServiceBase, IMaximsAppService
    {
        IRepository<Maxim> _repository;

        public MaximsAppService(IRepository<Maxim> repository)
        {
            _repository = repository;
        }

        public PagedResultDto<MaximDto> GetAll(GetAllMaximInput input)
        {
            List<Maxim> records;
            int count;

            if (string.IsNullOrEmpty(input.Title))
            {
                records = _repository.GetAll().OrderBy(f => f.Title).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                count = _repository.Count();
            }
            else
            {
                records = _repository.GetAll().Where(f => f.Title.Contains(input.Title)).OrderBy(f => f.Title).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                count = _repository.Count(f => f.Title.Contains(input.Title));
            }

            return new PagedResultDto<MaximDto>(count, records.MapTo<List<MaximDto>>());
        }
    }
}
