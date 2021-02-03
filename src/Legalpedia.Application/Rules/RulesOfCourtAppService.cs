using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Legalpedia.Rules.Dtos;
using Legalpedia.Models;
using System.Linq;
using Abp.AutoMapper;
using System.Collections.Generic;
using Abp.UI;

namespace Legalpedia.Rules
{
    public class RulesOfCourtAppService : LegalpediaAppServiceBase, IRulesOfCourtAppService
    {
        private readonly IRepository<Rule> _repository;

        public RulesOfCourtAppService(IRepository<Rule> repository)
        {
            _repository = repository;
        }

        public List<string> GetNames(string ruleType)
        {
            return _repository.GetAll().Where(r => r.Type == ruleType).Select(r => r.Name).Distinct().ToList();
        }

        public bool Create(RuleDto rule)
        {
            if (_repository.GetAll().Any(r => r.Name == rule.Name && r.Type == rule.Type && r.Section == rule.Section && r.Title == rule.Title))
            {
                throw new UserFriendlyException("A rule with the same name, type and section exists");
            }
            _repository.Insert(new Rule
            {
                Content = rule.Content, Name = rule.Name, Section = rule.Section, Title = rule.Title, Type = rule.Type,
                Uuid = rule.Uuid
            });

            return true;
        }

        public PagedResultDto<RuleDto> GetAll(GetAllRulesInput input)
        {
            var rules = _repository.GetAll().OrderBy(r => r.Id)
                .Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var totalCount = _repository.Count();
            return new PagedResultDto<RuleDto>(totalCount, rules.MapTo<List<RuleDto>>());
        }

        public PagedResultDto<RuleDto> State(GetAllRulesInput input)
        {
            var query = _repository.GetAll().Where(r => r.Type == "State" && r.Name == input.StateName);
            if (!string.IsNullOrEmpty(input.Section))
            {
                query = query.Where(r => r.Section == input.Section);
            }

            var rules = query.OrderBy(r => r.Id)
                .Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var totalCount = query.Count();
            return new PagedResultDto<RuleDto>(totalCount, rules.MapTo<List<RuleDto>>());
        }

        public PagedResultDto<RuleDto> Other(GetAllRulesInput input)
        {
            var query = _repository.GetAll().Where(r => r.Type == "Other" && r.Name == input.StateName);
            if (!string.IsNullOrEmpty(input.Section))
            {
                query = query.Where(r => r.Section == input.Section);
            }

            var rules = query.OrderBy(r => r.Id)
                .Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var totalCount = query.Count();
            return new PagedResultDto<RuleDto>(totalCount, rules.MapTo<List<RuleDto>>());
        }

        public RuleDto Detail(EntityDto<string> input)
        {
            var rule = _repository.FirstOrDefault(r => r.Uuid == input.Id);
            return rule == null ? null : rule.MapTo<RuleDto>();
        }
       
    }
}
