using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Legalpedia.Models;
using Legalpedia.Searches.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Timing;
using Abp.UI;
using CsQuery;
using Legalpedia.Users.Dto;
using Index = Legalpedia.Models.Index;
using Keyword = Legalpedia.Models.Keyword;
using KeywordRanking = Legalpedia.Models.KeywordRanking;

namespace Legalpedia.Searches
{
    public class SearchAppService:LegalpediaAppServiceBase, ISearchAppService
    {
        private readonly IRepository<Keyword> _keywordRepository;
        private readonly IRepository<SearchHistory> _searchHistoryRepository;
        private readonly IRepository<KeywordRanking> _keywordRankingRepository;
        private readonly IRepository<SummaryRatio> _ratioRepository;
        private readonly IRepository<JudgementsSummary, string> _summaryRepository;
        
        public SearchAppService(IRepository<Keyword> keywordRepository, IRepository<SearchHistory> searchHistoryRepository, 
            IRepository<SummaryRatio> ratioRepository, IRepository<JudgementsSummary, string> summaryRepository, IRepository<KeywordRanking> keywordRankingRepository)
        {
            _keywordRepository = keywordRepository;
            _searchHistoryRepository = searchHistoryRepository;
            _ratioRepository = ratioRepository;
            _summaryRepository = summaryRepository;
            _keywordRankingRepository = keywordRankingRepository;
        }

        public PagedResultDto<KeywordDto> GetAll(PagedResultRequestDto input)
        {
            var keywords = _keywordRepository.GetAll().OrderBy(k => k.Text).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var count = _keywordRepository.Count();

            return new PagedResultDto<KeywordDto>(count, keywords.MapTo<List<KeywordDto>>());
        }

        public bool AddKeyword(AddKeywordInput input)
        {
            var existingKeyword = _keywordRepository.FirstOrDefault(k => k.Text.ToLower() == input.Text.ToLower());
            if (existingKeyword != null)
            {
                // TODO: search for content update and conduct search if necssry
                existingKeyword.ResultCount = input.ResultCount;
                existingKeyword.SummaryCount = input.SummaryCount;
                existingKeyword.RatioCount = input.RatioCount;
                _keywordRepository.Update(existingKeyword);
                return true;
            }

            // TODO: conduct a search for this keyword
            var keyword = new Keyword
            {
                Text = input.Text,
                ResultCount = input.ResultCount,
                SummaryCount = input.SummaryCount,
                RatioCount = input.RatioCount,
                LastIndexingDate = DateTime.Now,
                Version = DateTime.Now,
            };
            _keywordRepository.Insert(keyword);

            return true;
        }

        public async  Task<SearchResult> Search(SearchInput input)
        {
            try
            {

                if (string.IsNullOrEmpty(input.Term)) throw new UserFriendlyException("Search term is required");
                var term = input.Term.ToLower();
                var startTime = Clock.Now;

                var currentUser = new UserDto {Id = 1}; // await GetCurrentUserAsync();

                // var history = 
                //     _searchHistoryRepository.FirstOrDefault(s =>
                //         s.SearchWord == term && s.CreatorUserId == currentUser.Id);
                // if (history != null)
                // {
                //     _searchHistoryRepository.Delete(history);
                // }
                //
                // history = new SearchHistory
                // {
                //     SearchWord = term
                // };
                // _searchHistoryRepository.Insert(history);

                var keyword = _keywordRepository.FirstOrDefault(k => k.Text == term);
                if (keyword == null || keyword.ResultCount == 0)
                {
                    ComputeSearch(term);
                    UnitOfWorkManager.Current.SaveChanges();

                    keyword = _keywordRepository.FirstOrDefault(k => k.Text == term);
                    if (keyword == null)
                        throw new UserFriendlyException("An unusual problem has occured during the search operation");
                }

                var query = _keywordRankingRepository.GetAll().Where(kr => kr.KeywordId == keyword.Id);
                if (input.Type != IndexType.None)
                {
                    query = query.Where(kr => kr.Type == input.Type);
                }

                var result = query.OrderByDescending(kr => kr.Rank)
                    .Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

                if (result.Count > 0 && keyword.ResultCount == 0)
                {
                    keyword.ResultCount = _keywordRankingRepository.Count(kr => kr.KeywordId == keyword.Id);
                    keyword.RatioCount =
                        _keywordRankingRepository.Count(kr => kr.KeywordId == keyword.Id && kr.Type == IndexType.Ratio);
                    keyword.SummaryCount = _keywordRankingRepository.Count(kr =>
                        kr.KeywordId == keyword.Id && kr.Type == IndexType.Summary);
                    _keywordRepository.Update(keyword);
                    UnitOfWorkManager.Current.SaveChanges();
                }

                var total = keyword.ResultCount;
                switch (input.Type)
                {
                    case IndexType.Ratio:
                        total = keyword.RatioCount;
                        break;
                    case IndexType.Summary:
                        total = keyword.SummaryCount;
                        break;
                }

                var records = new List<Index>();
                foreach (var kr in result)
                {
                    records.Add(new Index
                    {
                        SuitNo = kr.SuitNo,
                        Title = kr.Title,
                        Subbody = kr.Subbody,
                        Type = kr.Type,
                        Court = kr.Court,
                        Header = kr.Header,
                        JudgeDate = kr.JudgeDate
                    });
                }

                var duration = DateTime.Now - startTime;

                return new SearchResult(total, records, duration);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void ComputeSearch(string searchTerm)
        {
            var keyword = _keywordRepository.FirstOrDefault(k => k.Text == searchTerm);
            if (keyword == null)
            {
                keyword = new Keyword {LastIndexingDate = DateTime.Now, Text = searchTerm};
                var id = _keywordRepository.InsertAndGetId(keyword);
            }
            //exact match title
            var records = _ratioRepository.GetAll().Where(ind => ind.Heading.ToLower() == keyword.Text.ToLower()).ToList();
            foreach (var record in records)
            {
                RankAndSaveResult(keyword, record, 1000000000000);
            }
            
            //contained in title
            records = _ratioRepository.GetAll().Where(ind =>ind.Heading.ToLower() != keyword.Text.ToLower() && ind.Heading.ToLower().Contains(keyword.Text.ToLower())).ToList();
            foreach (var record in records)
            {
                RankAndSaveResult(keyword, record, 10000);
            }
            
            var resultCount = _keywordRankingRepository.Count(kr => kr.KeywordId == keyword.Id);
            if (resultCount > 100)
            {
                keyword.ResultCount = resultCount;
                keyword.RatioCount = _keywordRankingRepository.Count(kr => kr.KeywordId == keyword.Id && kr.Type == IndexType.Ratio);
                keyword.SummaryCount = _keywordRankingRepository.Count(kr => kr.KeywordId == keyword.Id && kr.Type == IndexType.Summary);
                _keywordRepository.Update(keyword);
                return;
            }
            
            _ratioRepository.GetAll().Where(ind => (ind.Heading.ToLower() != keyword.Text.ToLower() && !ind.Heading.ToLower().Contains(keyword.Text.ToLower())) && ind.Body.ToLower().Contains(keyword.Text.ToLower())).ToList();
            foreach (var record in records)
            {
                RankAndSaveResult(keyword, record, 1);
            }


            resultCount = _keywordRankingRepository.Count(kr => kr.KeywordId == keyword.Id);
            keyword.ResultCount = resultCount;
            keyword.RatioCount = _keywordRankingRepository.Count(kr => kr.KeywordId == keyword.Id && kr.Type == IndexType.Ratio);
            keyword.SummaryCount = _keywordRankingRepository.Count(kr => kr.KeywordId == keyword.Id && kr.Type == IndexType.Summary);
        }
        
        private void RankAndSaveResult(Keyword keyword, SummaryRatio result, long density)
        {
            if (string.IsNullOrEmpty(result.Body))
            {
                return;
            }

            var keywordRanking =
                _keywordRankingRepository.FirstOrDefault(
                    kr => kr.KeywordId == keyword.Id && kr.IndexId == result.Id);
            if (keywordRanking != null)
            {
                //TODO confirm from business owner if density should be considered
                /*keywordRanking.Rank += density;
                DbContext.SaveChanges();*/
                return;
            }

            var summary = _summaryRepository.GetAllIncluding(s=>s.Court).FirstOrDefault(s => s.Id == result.SuitNo);

            keywordRanking = new KeywordRanking
            {
                KeywordId = keyword.Id,
                IndexId = result.Id,
                SuitNo = result.SuitNo,
                Title = summary?.Title,
                Court = summary?.Court?.Name,
                Header = result.Heading,
                JudgeDate = summary?.JudgementDate?.ToLongDateString(),
                Subbody = result.Body,
                Type = IndexType.Ratio
            };

            //title scord
            var docment = CQ.CreateDocument(result.Heading);
            var headerText = docment.Text();
            //TODO confirm that the business wants the split - a1
            var headerLength = headerText.Contains("-") ? headerText.Split('-')[1].Length : headerText.Length;
            headerLength = headerText.Length; //TODO remove this if a1 - a2
            if (headerLength == 0) headerLength = 1;
            keywordRanking.Rank = density + 9000000000/headerLength;
            keywordRanking.Rank += GetScoreFromDateString(summary?.JudgementDate);

            _keywordRankingRepository.InsertAndGetId(keywordRanking);
        }

        private long GetScoreFromDateString(DateTime? resultJudgeDate)
        {
            try
            {
                var date = Convert.ToDateTime(resultJudgeDate);
                var dayDiff = DateTime.Now.Subtract(date).Days;
                return 200000 / dayDiff;

            }
            catch (Exception exception)
            {
                return 0;
            }
        }
    }
}
