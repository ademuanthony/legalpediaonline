using Legalpedia.Models;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Legalpedia.FormsAndPrecedence.Dtos;
using System.Linq;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace Legalpedia.FormsAndPrecedence
{
    public class FormsPrecedenceAppService : LegalpediaAppServiceBase, IFormsPrecedenceAppService
    {
        IRepository<FormsPrecedence> _repository;

        public FormsPrecedenceAppService(IRepository<FormsPrecedence> repository)
        {
            _repository = repository;
        }

        public FormsAndPrecedenceDto Create(FormsAndPrecedenceDto input)
        {
            var oldRecord = _repository.FirstOrDefault(f => f.Title == input.Title && f.Uuid == input.Uuid);
            if (oldRecord != null)
            {
                oldRecord.Content = input.Content;
                oldRecord.Category = input.Category;
                _repository.Update(oldRecord);

                input.Id = oldRecord.Id;
                return input;
            }

            input.Id = _repository.InsertAndGetId(new FormsPrecedence
            {
                Uuid = input.Uuid,
                Category = input.Category,
                Content = input.Content,
                Title = input.Title
            });
            return input;
        }

        public PagedResultDto<FormsAndPrecedenceDto> GetAll(GetAllFormsAndPrecedenceInput input)
        {
            List<FormsPrecedence> records;
            int count;

            var query = _repository.GetAll();
            if (!string.IsNullOrEmpty(input.Category))
            {
                query = query.Where(f => f.Category == input.Category);
            }

            if (!string.IsNullOrEmpty(input.Title))
            {
                query = query.Where(f => f.Title.ToLower().Contains(input.Title.ToLower()));
            }
            
            records = query.OrderBy(f => f.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            count = query.Count();

            return new PagedResultDto<FormsAndPrecedenceDto>(count, records.MapTo<List<FormsAndPrecedenceDto>>());
        }

        public FormsAndPrecedenceDto Detail(EntityDto<string> input)
        {
            var record = _repository.FirstOrDefault(f => f.Uuid == input.Id);
            return record.MapTo<FormsAndPrecedenceDto>();
        }

        public List<string> GetCategories()
        {
            var categories = _repository.GetAll().Select(f => f.Category).Distinct().OrderBy(s=>s).ToList();
            return categories;
        }
    }
}
