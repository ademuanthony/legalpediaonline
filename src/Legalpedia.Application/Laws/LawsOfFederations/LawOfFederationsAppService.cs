using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Legalpedia.Laws.LawsOfFederations.Dtos;
using Legalpedia.Models;
using Legalpedia.Dto;
using Legalpedia.EntityFrameworkCore.Repositories;

namespace Legalpedia.Laws.LawsOfFederations
{
    public class LawsOfFederationAppService : AsyncCrudAppService<LawOfFederation,
        LawOfFederationDto, int,
        PagedResultRequestDto, CreateLawOfFederationDto, UpdateLawOfFederationDto>, ILawsOfFederationAppService
    {
        private readonly IRepository<LawOfFedSched> _schedulesRepository;
        private readonly ILawSectionRepository _sectionRepository;
        private readonly IRepository<LawOfFedPart> _partRepository;

        public LawsOfFederationAppService(IRepository<LawOfFederation> repository, 
            IRepository<LawOfFedSched> schedulesRepository, ILawSectionRepository sectionRepository,
            IRepository<LawOfFedPart> partRepository) : base(repository)
        {
            _schedulesRepository = schedulesRepository;
            _sectionRepository = sectionRepository;
            _partRepository = partRepository;
        }

        public bool Post(LawOfFederation model)
        {
            if(!string.IsNullOrEmpty(model.LawNo))
                if (Repository.GetAll().Any(l => l.LawNo == model.LawNo))
                {
                    throw new UserFriendlyException("Law number already in use");
                }

            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;

            var id = Repository.InsertAndGetId(model);
            foreach (var lawOfFedPart in model.Parts)
            {
                lawOfFedPart.LawId = id;
                var partId = _partRepository.InsertAndGetId(lawOfFedPart);
                foreach (var lawOfFedSection in lawOfFedPart.Sections)
                {
                    lawOfFedSection.PartId = partId;
                    _sectionRepository.Insert(lawOfFedSection);
                }
            }

            if(model.Schedules != null)
            foreach (var modelSchedule in model.Schedules)
            {
                modelSchedule.LawId = id;
                _schedulesRepository.Insert(modelSchedule);
            }

            return true;
        }

        public bool Put(LawOfFederation model)
        {
            if(model.LawNo != "")
                if (Repository.GetAll().Any(l => l.LawNo == model.LawNo && l.Id != model.Id))
                {
                    throw new UserFriendlyException("Law number already in use");
                }

            var oldLaw = Repository.FirstOrDefault(model.Id);
            if(oldLaw == null) throw new UserFriendlyException("Law Not found");

            oldLaw.LawNo = model.LawNo;
            oldLaw.CatId = model.CatId;
            oldLaw.Descr = model.Descr;
            oldLaw.LawDate = model.LawDate;
            oldLaw.SubsidiaryLegislation = model.SubsidiaryLegislation;
            oldLaw.Title = model.Title;
            oldLaw.Tags = model.Tags;
            oldLaw.UpdatedAt = DateTime.Now;

            Repository.Update(oldLaw);

            foreach (var lawOfFedPart in model.Parts)
            {
                lawOfFedPart.LawId = model.Id;
                var partId = lawOfFedPart.Id == 0
                    ? _partRepository.InsertAndGetId(lawOfFedPart)
                    : _partRepository.InsertOrUpdateAndGetId(lawOfFedPart);

                foreach (var lawOfFedSection in lawOfFedPart.Sections)
                {
                    lawOfFedSection.PartId = partId;
                    if(lawOfFedSection.Id == 0)
                    _sectionRepository.Insert(lawOfFedSection);
                    else
                    {
                        _sectionRepository.Update(lawOfFedSection);
                    }
                }
            }

            foreach (var modelSchedule in model.Schedules)
            {
                modelSchedule.LawId = model.Id;
                if (modelSchedule.Id == 0)
                    _schedulesRepository.Insert(modelSchedule);
                else _schedulesRepository.Update(modelSchedule);
            }

            return true;
        }

        public PagedResultDto<LawOfFederationDto> Filter(FilterInput input)
        {
            var query = Repository.GetAll();
            if (!string.IsNullOrEmpty(input.Title))
            {
                query = query.Where(l => l.Title.ToLower().Contains(input.Title.ToLower()));
            }

            if (input.Year > 0)
            {
                query = query.Where(l => l.LawDate.Year == input.Year);
            }

            var records = query.OrderBy(l => l.Title).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var totalCount = query.Count();

            return new PagedResultDto<LawOfFederationDto>(totalCount, records.MapTo<List<LawOfFederationDto>>());
        }

        public PagedResultDto<LawSearchResult> Search(FilterInput input)
        {
            var sections = _sectionRepository.GetAll()
                .Where(s => s.SectionHeader.ToLower().Contains(input.Title.ToLower()))
                .Select(s => new LawSearchResult{
                    SectionHeader = s.SectionHeader,
                    SectionBody = s.SectionBody,
                    LawId = s.LawId,
                })
                .OrderBy(s=>s.SectionHeader.Length)
                .Skip(input.SkipCount)
                .Take((input.MaxResultCount)).ToList();

            foreach (var section in sections)
            {
                var law = Repository.FirstOrDefault(l => l.Id == section.LawId);
                section.LawTitle = law?.Title;
            }

            var totalCount = _sectionRepository.Count(s => s.SectionHeader.ToLower().Contains(input.Title.ToLower()));
            
            return new PagedResultDto<LawSearchResult>(totalCount, sections);
        }
        
        public LawOfFederation Detail(EntityDto input)
        {
            var law = Repository.Get(input.Id);
            
            var parts = _partRepository.GetAll().Where(p => p.LawId == law.Id).OrderBy(p => p.PartHeader).ToList();
            foreach (var part in parts)
            {
                var sections = _sectionRepository.GetAll().Where(s => s.PartId == part.Id).OrderBy(s=>s.SectionHeader);
                part.Sections = sections.ToList();
            }

            law.Parts = parts;
            var schedules = _schedulesRepository.GetAll().Where(s => s.LawId == law.Id).ToList();
            law.Schedules = schedules;

            return law;
        }
    }
}

