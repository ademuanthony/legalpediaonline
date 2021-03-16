using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Castle.Core.Internal;
using Legalpedia.EntityFrameworkCore.Repositories;
using Legalpedia.Judgements;
using Legalpedia.Models;
using Legalpedia.Dto;
using JudgementPartiesA = Legalpedia.Models.JudgementPartiesA;
using JudgementPartiesB = Legalpedia.Models.JudgementPartiesB;
using UploadResultDto = Legalpedia.Shared.Dto.UploadResultDto;
using System.ComponentModel.DataAnnotations;
using Legalpedia.Judgements.Dto;
using Legalpedia.Summaries.Dtos;

namespace Legalpedia.Summaries
{
    public class SummariesAppService:LegalpediaAppServiceBase, ISummariesAppService
    {
        #region ppts
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<SummaryRatio> _rationRepository;
        private readonly ISummaryRepository _summaryRepository;
        private readonly IRepository<Judgement, string> _judgmentRepository;
        private readonly IRepository<JudgementCoram> _judgementCoramsRepository;
        private readonly IRepository<Principle> _principleRepository;
        private readonly IRepository<JudgementPrinciple> _judgementPrinciplesRepository;
        private readonly IRepository<SbjMatterIndex> _smiRepository;
        private readonly IRepository<JudgementPartiesA, string> _judgementPartiesARepository;
        private readonly IRepository<JudgementPartiesB, string> _judgementPartiesBRepository;
        private readonly IRepository<PartyAType> _partyATypeRepository;
        private readonly IRepository<PartyBType> _partyBTypeRepository;
        private readonly IRepository<Tag> _tagsRepository;
        private readonly IRepository<JudgementCounsel, string> _counselRepository;
        private readonly IRepository<HoldenAt> _holdenAtRepository;
        private readonly IRepository<Court> _courtRepository;
        private readonly IRepository<Coram> _coramRepository;
        private readonly IRepository<JudgementCoram> _judgementCoramRepository;
        private readonly IRepository<JudgementCounsel, string> _judgementCounselRepository;
        private readonly IRepository<SumAreasOfLaw> _sumAreasOfLaw;
        private readonly IJudgementsAppService _judgementsAppService;
        private readonly IRepository<JudgementPage, string> _pagesRepository;
        #endregion

        #region cntr
        public SummariesAppService(
            IRepository<SummaryRatio> rationRepository,
            ISummaryRepository summaryRepository, 
            IRepository<JudgementCoram> judgementCoramsRepository, 
            IRepository<JudgementPrinciple> judgementPrinciplesRepository,
            IRepository<Judgement, string> judgmentRepository,
            IRepository<JudgementPartiesA, string> judgementPartiesARepository, 
            IRepository<JudgementPartiesB, string> judgementPartiesBRepository,
            IRepository<PartyAType> partyATypeRepository,
            IRepository<PartyBType> partyBTypeRepository,
            IRepository<Tag> tagsRepository, 
            IRepository<JudgementCounsel, string> counselRepository, 
            IRepository<HoldenAt> holdenAtRepository,
            IRepository<Court> courtRepository, IRepository<Coram> coramRepository, 
            IRepository<JudgementCoram> judgementCoramRepository, 
            IRepository<JudgementCounsel, string> judgementCounselRepository, 
            IRepository<SumAreasOfLaw> sumAreasOfLaw, IJudgementsAppService judgementsAppService, 
            IRepository<SbjMatterIndex> smiRepository, IRepository<Principle> principleRepository,
            IRepository<Category> categoryRepository, IRepository<JudgementPage, string> pagesRepository)
        {
            _categoryRepository = categoryRepository;
            _pagesRepository = pagesRepository;
            _rationRepository = rationRepository;
            _summaryRepository = summaryRepository;
            _judgementCoramsRepository = judgementCoramsRepository;
            _judgmentRepository = judgmentRepository;
            _judgementPrinciplesRepository = judgementPrinciplesRepository;
            _judgementPartiesARepository = judgementPartiesARepository;
            _judgementPartiesBRepository = judgementPartiesBRepository;
            _partyATypeRepository = partyATypeRepository;
            _partyBTypeRepository = partyBTypeRepository;
            _tagsRepository = tagsRepository;
            _counselRepository = counselRepository;
            _holdenAtRepository = holdenAtRepository;
            _counselRepository = counselRepository;
            _courtRepository = courtRepository;
            _coramRepository = coramRepository;
            _judgementCoramRepository = judgementCoramRepository;
            _judgementCounselRepository = judgementCounselRepository;
            _sumAreasOfLaw = sumAreasOfLaw;
            _judgementsAppService = judgementsAppService;
            _smiRepository = smiRepository;
            _principleRepository = principleRepository;
        }
        #endregion

        public JudgementSummaryDto GetForEdit(EntityDto<string> input)
        {
            var summary = _summaryRepository.GetAll().FirstOrDefault(s => s.Id==input.Id);
            if(summary == null) throw new UserFriendlyException("Summary not found");
            var summaryDto = new JudgementSummaryDto
            {
                Summary = summary,
                Ration = _rationRepository.GetAll().Where(r => r.SuitNo == summary.Id).ToList(),
                Corams = _judgementCoramRepository.GetAll().Where(jc => jc.JudgementId == summary.Id).Select(jc=>jc.Coram).ToList(),
                Counsel = _judgementCounselRepository.GetAll().FirstOrDefault(jc => jc.Id == summary.Id),
                PartiesA = _judgementPartiesARepository.GetAll().FirstOrDefault(jp => jp.Id == summary.Id),
                PartiesB = _judgementPartiesBRepository.GetAll().FirstOrDefault(jp => jp.Id == summary.Id),
                HoldenAt = _holdenAtRepository.GetAll().FirstOrDefault(h => h.Id == summary.HoldenAtId),
                Principles = _judgementPrinciplesRepository.GetAll().Where(jp => jp.SuitNo == summary.Id).ToList(),
                AreasOfLaws = _sumAreasOfLaw.GetAll().Where(ar => ar.SuitNo == input.Id).Select(ar => ar.AreaOfLaw).ToList()
                
            };

            return summaryDto;
        }

        public UploadResultDto Post(UploadSummaryInput input)
        {

            var result = new UploadResultDto();

            try
            {
                var court = _courtRepository.FirstOrDefault(c => c.Id == input.Summary.CourtId);
                if (court == null)
                {
                    throw new UserFriendlyException("Invalid Court Id");
                }

                if (_summaryRepository.GetAll().Any(s => s.Id == input.Summary.Id))
                {
                    if (input.JudgementBody.IsNullOrEmpty())//the upload from template passes judgement body
                    {
                        throw new UserFriendlyException("Selected suit number already in use");
                    }

                    return Put(input);
                }

                if (!input.JudgementBody.IsNullOrEmpty())
                {
                    _judgementsAppService.Post(new JudgementDto { Body = input.JudgementBody, Id = input.Summary.Id });
                }

                //fetch the summary
                var summary = new JudgementsSummary
                {
                    Id = input.Summary.Id,
                    CategoryId = input.Summary.CategoryId,
                    CourtId = input.Summary.CourtId,
                    Held = input.Summary.Held,
                    PartyATypeId = input.Summary.PartyATypeId,
                    PartyBTypeId = input.Summary.PartyBTypeId,
                    Issues = input.Summary.Issues,
                    JudgementDate = input.Summary.JudgementDate,
                    LpCitation = input.Summary.LpCitation,
                    Title = input.Summary.Title,
                    OtherCitations = input.Summary.OtherCitations,
                    SummaryOfFacts = input.Summary.SummaryOfFacts,
                    Tags = "",
                    UpdatedAt = DateTime.Now,
                };

                if (string.IsNullOrEmpty(summary.LpCitation))
                {
                    summary.LpCitation = GenerateCitation(summary, court);
                }

                //area of law
                if (input.AreasOfLaws != null && input.AreasOfLaws.Count > 0)
                {
                    foreach (var areaOfLaw in input.AreasOfLaws)
                    {
                        var sumAreaOfLaw = new SumAreasOfLaw
                        {
                            AreaOfLawID = areaOfLaw.Id, SuitNo = summary.Id,
                        };
                        _sumAreasOfLaw.Insert(sumAreaOfLaw);
                    }

                }

                //and tags
                if(input.Tags != null)
                    foreach (var tag in input.Tags)
                    {
                        if (!_tagsRepository.GetAll().Any(t => t.Text == tag))
                        {
                            _tagsRepository.Insert(new Tag {Text = tag});
                        }

                        summary.Tags += !string.IsNullOrEmpty(summary.Tags) ? $"|{tag}" : tag;
                    }

                //cases cited
                if (input.CasesCited != null)
                    foreach (var caseCited in input.CasesCited)
                    {
                        summary.CasesCited += !string.IsNullOrEmpty(summary.CasesCited) ? $"|{caseCited}" : caseCited;
                    }

                //Status Cited
                if (input.StatusCited != null)
                    foreach (var statusCited in input.StatusCited)
                    {
                        summary.StatusCited += !string.IsNullOrEmpty(summary.StatusCited)
                            ? $"|{statusCited.Title}"
                            : statusCited.Title;
                    }

                _summaryRepository.Insert(summary);


                //save parties a
                var partiesA = new JudgementPartiesA
                {
                    Id = input.Summary.Id,
                };
                foreach (var name in input.PartyTypeANames)
                {
                    partiesA.PartyANames += !string.IsNullOrEmpty(partiesA.PartyANames) ? $"|{name}" : name;
                }

                _judgementPartiesARepository.Insert(partiesA);
                //parties b
                var partiesB = new JudgementPartiesB
                {
                    Id = input.Summary.Id,
                };
                foreach (var name in input.PartyTypeBNames)
                {
                    partiesB.PartyBNames += !string.IsNullOrEmpty(partiesB.PartyBNames) ? $"|{name}" : name;
                }

                _judgementPartiesBRepository.Insert(partiesB);

                //corams
                foreach (var coram in input.Corams)
                {
                    if (coram.Id == 0)
                    {
                        var dbCoram = _coramRepository.FirstOrDefault(c => c.Name.ToLower() == coram.Name.ToLower());
                        if (dbCoram == null)
                        {
                            dbCoram = new Coram {Name = coram.Name,};
                            dbCoram.Id = _coramRepository.InsertAndGetId(dbCoram);
                        }

                        coram.Id = dbCoram.Id;
                    }

                    var judgementCoram = new JudgementCoram
                    {
                        CoramId = coram.Id,
                        JudgementId = summary.Id,
                    };
                    _judgementCoramsRepository.Insert(judgementCoram);
                }

                //judgement principle
                var judgmentPrinciple = new JudgementPrinciple
                {
                    PrincipleId = input.PrincipleId,
                    SuitNo = input.Summary.Id,
                };
                _judgementPrinciplesRepository.Insert(judgmentPrinciple);
                //ration
                foreach (var ratioDto in input.Ration)
                {
                    var ratio = new SummaryRatio
                    {
                        Body = ratioDto.Body,
                        Heading = ratioDto.Heading,
                        SuitNo = input.Summary.Id,
                    };
                    _rationRepository.Insert(ratio);

                    // SMI and principle
                    if (ratio.Smis == null) continue;
                    foreach (var smi in ratio.Smis)
                    {
                        var sbjMatterIndex = _smiRepository.FirstOrDefault(s => s.SubjectMatterIndex == smi);
                        if (sbjMatterIndex == null)
                        {
                            sbjMatterIndex = new SbjMatterIndex
                            {
                                SubjectMatterIndex = smi,
                                UpdatedAt = DateTime.Now
                            };
                            sbjMatterIndex.Id = _smiRepository.InsertAndGetId(sbjMatterIndex);
                        }

                        var principle = _principleRepository.FirstOrDefault(p =>
                            p.Name == ratioDto.Heading && p.SbjMatterIdxId == sbjMatterIndex.Id);
                        if (principle == null)
                        {
                            principle = new Principle
                            {
                                Name = ratioDto.Heading,
                                SbjMatterIdxId = sbjMatterIndex.Id,
                                UpdatedAt = DateTime.Now
                            };
                            principle.Id = _principleRepository.InsertAndGetId(principle);
                        }

                        if (!_judgementPrinciplesRepository.GetAll()
                            .Any(p => p.PrincipleId == principle.Id && p.SuitNo == input.Summary.Id))
                        {
                            _judgementPrinciplesRepository.InsertAndGetId(new JudgementPrinciple
                            {
                                PrincipleId = principle.Id,
                                SuitNo = input.Summary.Id
                            });
                        }
                    }
                }

                //counsels
                var counsels = new JudgementCounsel
                {
                    Id = summary.Id,
                };
                foreach (var counsel in input.Counsels)
                {
                    counsels.Counsels += !string.IsNullOrEmpty(counsels.Counsels) ? $"|{counsel}" : counsel;
                }

                _counselRepository.Insert(counsels);

                //holdens
                foreach (var holdenAt in input.HoldenAts)
                {
                    summary.HoldenAtId = _holdenAtRepository.InsertAndGetId(new HoldenAt
                    {
                        Name = holdenAt,
                    });
                }
            }
            catch (ValidationException dbEx)
            {
                Exception raise = dbEx;
                var message = $"{dbEx.ValidationResult.MemberNames}:{dbEx.ValidationResult.ErrorMessage}";

                raise = new InvalidOperationException(message, raise);
                throw raise;
            }

            return result;
        }

        public UploadResultDto Put(UploadSummaryInput input)
        {
            var result = new UploadResultDto();

            var court = _courtRepository.FirstOrDefault(c => c.Id == input.Summary.CourtId);
            if (court == null)
            {
                throw new UserFriendlyException("Invalid Court Id");
            }

            var summary = _summaryRepository.FirstOrDefault(s => s.Id == input.Summary.Id);
            if(summary == null)
            {
                throw new UserFriendlyException("Invalid suit number");
            }

            if (!input.JudgementBody.IsNullOrEmpty())
            {
                _judgementsAppService.Put(new JudgementDto { Body = input.JudgementBody, Id = input.Summary.Id});
            }

            //fetch the summary
            summary.Id = input.Summary.Id;
            summary.CategoryId = input.Summary.CategoryId;
            summary.CourtId = input.Summary.CourtId;
            summary.Held = input.Summary.Held;
            summary.PartyATypeId = input.Summary.PartyATypeId;
            summary.PartyBTypeId = input.Summary.PartyBTypeId;
            summary.Issues = input.Summary.Issues;
            summary.JudgementDate = input.Summary.JudgementDate;
            if (string.IsNullOrEmpty(summary.LpCitation))
            {
                summary.LpCitation = input.Summary.LpCitation; 
                if (string.IsNullOrEmpty(summary.LpCitation))
                {
                    summary.LpCitation = GenerateCitation(summary, court);
                }
            }
            summary.Title = input.Summary.Title;
            summary.OtherCitations = input.Summary.OtherCitations;
            summary.SummaryOfFacts = input.Summary.SummaryOfFacts;
            summary.UpdatedAt = DateTime.Now;//todo add updated at and remove this


            //area of law
            _sumAreasOfLaw.Delete(ar => ar.SuitNo == summary.Id);
            if (input.AreasOfLaws != null && input.AreasOfLaws.Count > 0)
            {
                foreach (var areaOfLaw in input.AreasOfLaws)
                {
                    var sumAreaOfLaw = new SumAreasOfLaw
                    {
                        AreaOfLawID = areaOfLaw.Id,
                        SuitNo = summary.Id,
                    };
                    _sumAreasOfLaw.Insert(sumAreaOfLaw);
                }
            }


            //and tags
            summary.Tags = "";
            foreach (var tag in input.Tags)
            {
                if (!_tagsRepository.GetAll().Any(t => t.Text == tag))
                {
                    _tagsRepository.Insert(new Tag { Text = tag });
                }
                summary.Tags += !string.IsNullOrEmpty(summary.Tags) ? $"|{tag}" : tag;
            }

            //cases cited
            summary.CasesCited = "";
            if (input.CasesCited != null)
                foreach (var caseCited in input.CasesCited)
                {
                    summary.CasesCited += !string.IsNullOrEmpty(summary.CasesCited) ? $"|{caseCited}" : caseCited;
                }
            //Status Cited
            summary.StatusCited = "";
            if (input.StatusCited != null)
                foreach (var statusCited in input.StatusCited)
                {
                    summary.StatusCited += !string.IsNullOrEmpty(summary.StatusCited) ? $"|{statusCited.Title}" : statusCited.Title;
                }

            _summaryRepository.Update(summary);

            //save parties a
            var partiesA = _judgementPartiesARepository.FirstOrDefault(p => p.Id == input.Summary.Id);
            bool isNew = false;
            if(partiesA == null)
            {
                isNew = true;
                partiesA = new JudgementPartiesA
                {
                    Id = input.Summary.Id,
                };
            }

            partiesA.PartyANames = "";

            foreach (var name in input.PartyTypeANames)
            {
                partiesA.PartyANames += !string.IsNullOrEmpty(partiesA.PartyANames) ? $"|{name}" : name;
            }
            if(isNew)
            {
                _judgementPartiesARepository.Insert(partiesA);
            }
            //parties b
            var partiesB = _judgementPartiesBRepository.FirstOrDefault(p => p.Id == input.Summary.Id);
            isNew = false;
            if (partiesB == null)
            {
                isNew = true;
                partiesB = new JudgementPartiesB
                {
                    Id = input.Summary.Id,
                };
            }

            partiesB.PartyBNames = "";
            
            foreach (var name in input.PartyTypeBNames)
            {
                partiesB.PartyBNames += !string.IsNullOrEmpty(partiesB.PartyBNames) ? $"|{name}" : name;
            }
            if (isNew)
            {
                _judgementPartiesBRepository.Insert(partiesB);
            }

            //corams
            _judgementCoramRepository.Delete(c => c.JudgementId == input.Summary.Id);
            foreach (var coram in input.Corams)
            {
                if (coram.Id == 0)
                {
                    var dbCoram = _coramRepository.FirstOrDefault(c => c.Name.ToLower() == coram.Name.ToLower());
                    if (dbCoram == null)
                    {
                        dbCoram = new Coram { Name = coram.Name, Version = DateTime.Now };
                        dbCoram.Id = _coramRepository.InsertAndGetId(dbCoram);
                    }

                    coram.Id = dbCoram.Id;
                }
                var judgementCoram = new JudgementCoram
                {
                    CoramId = coram.Id,
                    JudgementId = summary.Id,
                };
                _judgementCoramsRepository.Insert(judgementCoram);
            }
            //judgement principle
            _judgementPrinciplesRepository.Delete(j => j.SuitNo == input.Summary.Id);
            var judgmentPrinciple = new JudgementPrinciple
            {
                PrincipleId = input.PrincipleId,
                SuitNo = input.Summary.Id,
            };
            _judgementPrinciplesRepository.Insert(judgmentPrinciple);

            //ration
            _rationRepository.Delete(r => r.SuitNo == input.Summary.Id);
            foreach (var ratioDto in input.Ration)
            {
                var ratio = new SummaryRatio
                {
                    Body = ratioDto.Body,
                    Heading = ratioDto.Heading,
                    SuitNo = input.Summary.Id,
                    Coram = ratioDto.Coram
                };
                _rationRepository.Insert(ratio);

                // SMI and principle
                if (ratio.Smis != null)
                    foreach (var smi in ratio.Smis)
                    {
                        var sbjMatterIndex = _smiRepository.FirstOrDefault(s => s.SubjectMatterIndex == smi);
                        if (sbjMatterIndex == null)
                        {
                            sbjMatterIndex = new SbjMatterIndex
                            {
                                SubjectMatterIndex = smi,
                                UpdatedAt = DateTime.Now
                            };
                            sbjMatterIndex.Id = _smiRepository.InsertAndGetId(sbjMatterIndex);
                        }

                        var principle = _principleRepository.FirstOrDefault(p =>
                            p.Name == ratioDto.Heading && p.SbjMatterIdxId == sbjMatterIndex.Id);
                        if (principle == null)
                        {
                            principle = new Principle
                            {
                                Name = ratioDto.Heading,
                                SbjMatterIdxId = sbjMatterIndex.Id,
                                UpdatedAt = DateTime.Now
                            };
                            principle.Id = _principleRepository.InsertAndGetId(principle);
                        }

                        if (!_judgementPrinciplesRepository.GetAll()
                            .Any(p => p.PrincipleId == principle.Id && p.SuitNo == input.Summary.Id))
                        {
                            _judgementPrinciplesRepository.InsertAndGetId(new JudgementPrinciple
                            {
                                PrincipleId = principle.Id,
                                SuitNo = input.Summary.Id
                            });
                        }
                    }
            }
            //counsels
            _counselRepository.Delete(c => c.Id == summary.Id);
            var counsels = new JudgementCounsel
            {
                Id = summary.Id,
            };
            foreach (var counsel in input.Counsels)
            {
                counsels.Counsels += !string.IsNullOrEmpty(counsels.Counsels) ? $"|{counsel}" : counsel;
            }
            _counselRepository.Insert(counsels);

            //holdens
            _holdenAtRepository.Delete(h => h.Id == summary.HoldenAtId);
            foreach (var holdenAt in input.HoldenAts)
            {
                summary.HoldenAtId = _holdenAtRepository.InsertAndGetId(new HoldenAt
                {
                    Name = holdenAt, 
                });
            }
            
            return result;
        }

        public async Task<PagedResultDto<JudgementListItem>> GetListItems(GetJudgementRequest input)
        {
            var query = _summaryRepository.GetAll().Where(s => s.Id != "");
            if (!string.IsNullOrEmpty(input.Query))
                query = query.Where(s =>
                    s.Id.ToLower().Contains(input.Query.ToLower()) ||
                    s.Title.ToLower().Contains(input.Query.ToLower()));

            var totalCount = query.Count();

            var result = query.OrderBy(s => s.JudgementDate).Skip(input.SkipCount).Take(input.MaxResultCount)
                .Select(s => new JudgementListItem
                {
                    CaseTitle = s.Title,
                    //Court = s.Court.Name,
                    SuitNo = s.Id,
                    JudgementDate = s.JudgementDate
                }).ToList();

            return new PagedResultDto<JudgementListItem>(totalCount, result);
        }

        public PagedResultDto<JudgementSummaryViewModel> GetAll(GetAllSummaryInput input)
        {
            var query = _summaryRepository.GetAll();

            if (!string.IsNullOrEmpty(input.Title))
            {
                query = query.Where(s => s.Title.ToLower().Contains(input.Title.ToLower()) || s.Id.ToLower() == input.Title.ToLower());
            }

            if (!string.IsNullOrEmpty(input.TitlePrefix))
            {
                query = query.Where(s => s.Title.ToLower().StartsWith(input.TitlePrefix.ToLower()));
            }

            if (!string.IsNullOrEmpty(input.LpCitation))
            {
                query = query.Where(s => s.LpCitation.ToLower().Contains(input.LpCitation.ToLower()));
            }

            if (input.CourtId > 0)
            {
                query = query.Where(j => j.CourtId == input.CourtId);
            }

            var principles = new List<int>();

            if(input.SubjectMatterId > 0 && input.PrincipleId == 0)
            {
                principles = _principleRepository.GetAll().Where(p => p.SbjMatterIdxId == input.SubjectMatterId).Select(p => p.Id).ToList();
                if(principles.Count() == 0)
                {
                    return new PagedResultDto<JudgementSummaryViewModel>();
                }
            }

            if (input.PrincipleId > 0)
            {
                principles = new List<int> { input.PrincipleId };
            }

            var suitnos = new List<string>{ };
            if (principles.Count() > 0)
            {
                suitnos = _judgementPrinciplesRepository.GetAll().Where(jp => principles.Contains(jp.PrincipleId)).Select(p => p.SuitNo).ToList();
                if (suitnos.Count() == 0)
                {
                    return new PagedResultDto<JudgementSummaryViewModel>();
                }
            }

            if (suitnos.Count() > 0)
            {
                query = query.Where(j => suitnos.Contains(j.Id));
            }

            if (input.Year > 0)
            {
                query = query.Where(s => s.JudgementDate != null && s.JudgementDate.Value.Year == input.Year);
            }

            var summaries = query.OrderByDescending(s => s.JudgementDate).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var summaryDtos = new List<JudgementSummaryViewModel>();
            foreach (var summary in summaries)
            {
                summaryDtos.Add(SummaryToViewModel(summary));
            }

            var summaryCount = query.Count();
            return new PagedResultDto<JudgementSummaryViewModel>(summaryCount, summaryDtos);
        }

        public PagedResultDto<JudgementSummaryViewModel> Latest()
        {
            var oneMonthAgo = DateTime.Now.AddDays(-30);
            var query = _summaryRepository.GetAll().Where(s => s.JudgementDate != null && s.JudgementDate.Value >= oneMonthAgo);
            
            var summaries = query.OrderByDescending(s => s.JudgementDate).Skip(0).Take(30).ToList();
            var summaryDtos = new List<JudgementSummaryViewModel>();
            foreach (var summary in summaries)
            {
                summaryDtos.Add(SummaryToViewModel(summary));
            }

            var summaryCount = query.Count();
            return new PagedResultDto<JudgementSummaryViewModel>(summaryCount, summaryDtos);
        }

        public JudgementSummaryViewModel Details(EntityDto<string> input)
        {
            var summary = _summaryRepository.FirstOrDefault(s => s.Id == input.Id);
            if (summary == null)
            {
                throw new UserFriendlyException($"Case not found. {input.Id} is not a valid suit number");
            }

            var viewModel = SummaryToViewModel(summary);
            var judgement = _judgmentRepository.FirstOrDefault(j => j.Id == input.Id);
            if(judgement != null)
            {
                viewModel.JudgementBody = judgement.Body;
            }

            return viewModel;
        }

        public JudgementSummaryViewModel SummaryToViewModel(JudgementsSummary summary)
        {
            var areasOfLaws = _sumAreasOfLaw.GetAll().Where(ar => ar.SuitNo == summary.Id).Select(ar => ar.AreaOfLaw.Title).ToList();
            var category = _categoryRepository.FirstOrDefault(c => c.Id == summary.CategoryId);
            var Corams = _judgementCoramRepository.GetAll().Where(jc => jc.JudgementId == summary.Id)
                    .Select(jc => jc.Coram).ToList();
            var court = _courtRepository.FirstOrDefault(c => c.Id == summary.CourtId);
            var ration = _rationRepository.GetAll().Where(r => r.SuitNo == summary.Id).ToList();

            var counsel = _judgementCounselRepository.GetAll().FirstOrDefault(jc => jc.Id == summary.Id);
            var PartiesA = _judgementPartiesARepository.GetAll().FirstOrDefault(jp => jp.Id == summary.Id);
            var PartiesB = _judgementPartiesBRepository.GetAll().FirstOrDefault(jp => jp.Id == summary.Id);
            var partyAType = _partyATypeRepository.FirstOrDefault(p => p.Id == summary.PartyATypeId);
            var partyBType = _partyBTypeRepository.FirstOrDefault(p => p.Id == summary.PartyBTypeId);
            var HoldenAt = _holdenAtRepository.GetAll().FirstOrDefault(h => h.Id == summary.HoldenAtId);
            var principles = _judgementPrinciplesRepository.GetAll().Where(jp => jp.SuitNo == summary.Id).ToList();
            var pages = _pagesRepository.GetAll()
                .Where(p => p.SuitNumber == summary.Id)
                .OrderBy(p => p.Number)
                .ToList();

            var summaryDto = new JudgementSummaryViewModel
            {
                AreasOfLaw = areasOfLaws,
                CasesCited = summary.CasesCited,
                Category = category?.Name,
                Corams = Corams.Select(c => c.Name).ToList(),
                Counsels = counsel?.Counsels,
                Court = court?.Name,
                Held = summary.Held,
                HoldenAt = HoldenAt?.Name,
                Issues = summary.Issues,
                JudgementDate = summary.JudgementDate,
                LegalpediaCitation = summary.LpCitation,
                OtherCitations = summary.OtherCitations,
                PartiesA = PartiesA?.PartyANames,
                PartiesB = PartiesB?.PartyBNames,
                PartyAType = partyAType?.Name,
                PartyBType = partyBType?.Name,
                Ratios = ration.Select(r => new SummaryRatioDto
                {
                    Body = r.Body,
                    Coram = r.Coram,
                    Heading = r.Heading,
                    SuitNo = r.SuitNo
                }).ToList(),
                StatusCited = summary.StatusCited,
                SuitNo = summary.Id,
                SummaryOfFacts = summary.SummaryOfFacts,
                Title = summary.Title,
                Pages = pages,
            };

            return summaryDto;
        }
        
        
        public bool UpdateCitation(int start, int end)
        {
            //var count = _summaryRepository.Count() + 200;
            for(var i = start; i <= end; i+=200)
            {
                var suiteNos = _summaryRepository.GetAll().OrderByDescending(s=>s.JudgementDate).Select(s => s.Id).Skip(i).Take(200).ToList();
                foreach (var suiteNo in suiteNos)
                {
                    var summary = _summaryRepository.Get(suiteNo);
                    var court = _courtRepository.FirstOrDefault(c => c.Id == summary.CourtId.Value);
                    if (court == null) continue;
                    var lpCitation = GenerateCitation(summary, court);
                    if (lpCitation == string.Empty) continue;
                    summary.LpCitation = lpCitation;
                    summary.UpdatedAt = DateTime.Now;
                    _summaryRepository.Update(summary);
                }
                UnitOfWorkManager.Current.SaveChanges();
            }
            return true;
        }

        private string GenerateCitation(JudgementsSummary summary, Court court)
        {
            if (summary.JudgementDate == null) return string.Empty;
            
            string courtPrefix = string.Empty;
            switch (court.Name)
            {
                case Court.SupremeCourt:
                    courtPrefix = "SC";
                    break;
                case Court.CourtOfAppeal:
                    courtPrefix = "CA";
                    break;
                case Court.Tribunal:
                    courtPrefix = "IST";
                    break;
                case Court.NationalIndustrialCourt:
                    courtPrefix = "NICN";
                    break;
                case Court.ShariaCourt:
                    courtPrefix = "SCA";
                    break;
                case Court.FederalHighCourt:
                    courtPrefix = "FHC";
                    break;
                
            }

            if (courtPrefix == string.Empty) return string.Empty;
            
            var citationPrefix = $"({summary.JudgementDate.Value.Year}) Legalpedia ({courtPrefix}) ";

            var citation = string.Empty;
            while (citation == string.Empty)
            {
                var temp = citationPrefix + RandomNumber(5);
                if (!_summaryRepository.GetAll().Any(s => s.LpCitation == temp))
                    citation = temp;
            }

            return citation;
        }
        
        private string RandomNumber(int length)
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");
            var trucatedGuid = guid.Substring(guid.Length - length).ToCharArray();
            var randomStr = "";
            foreach(var c in trucatedGuid)
            {
                randomStr += Math.Abs(char.GetNumericValue(c));
            }
            return randomStr;
        }

        public string CitationUpdateSql()
        {
            var suiteNos = _summaryRepository.GetAll().Select(s => new {SuiteNo = s.Id, Citation = s.LpCitation}).ToList();
            var sqlBuilder = new StringBuilder();
            foreach (var suiteNo in suiteNos)
            {
                sqlBuilder.Append(
                    $"UPDATE JudgementsSummaries SET LpCitation='{suiteNo.Citation}' WHERE SuitNo = '{suiteNo.SuiteNo}';");
            }

            return sqlBuilder.ToString();
        }

        public string RatioBodyById(int ratioId)
        {
            var ration = _rationRepository.FirstOrDefault(r => r.Id == ratioId);
            return ration?.Body;
        }

        public string RationBodyByPrincipal(int principalId)
        {
            var principle = _principleRepository.FirstOrDefault(p => p.Id == principalId);
            if (principle == null) return null;
            return RatioBodyByTitle(principle.Name);
        }

        public string RatioBodyByTitle(string title)
        {
            var ratio = _rationRepository.FirstOrDefault(r => r.Heading.ToLower() == title.ToLower());
            return ratio?.Body;
        }
    }
}
