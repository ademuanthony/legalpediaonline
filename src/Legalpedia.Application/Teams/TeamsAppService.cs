﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Legalpedia.Authorization;
using Abp.UI;
using Legalpedia.Models;
using Abp.Authorization;
using Legalpedia.Teams.Dto;
using System.Net;
using Legalpedia.Users.Dto;
using Legalpedia.Authorization.Users;
using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using Abp.Extensions;
using Abp.Threading.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Legalpedia.Teams
{
   [AbpAuthorize]
    public class TeamsAppService : AsyncCrudAppService<Team,
        TeamDto, string,
        PagedResultRequestDto, CreateTeamDto, UpdateTeamDto>, ITeamsAppService
    {
        private readonly IRepository<TeamMember, string> _teamMemberRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<TeamLogo, string> _teamLogoRepository;
        public TeamsAppService(IRepository<Team, string> repository,
            IRepository<TeamMember, string> teamMemberRepository, IRepository<User, long> userRepository,
            IRepository<TeamLogo, string> teamLogoRepository) : base(repository)
        {
            _teamMemberRepository = teamMemberRepository;
            _userRepository = userRepository;
            _teamLogoRepository = teamLogoRepository;
        }

        public override Task<TeamDto> CreateAsync(CreateTeamDto input)
        {
            if (input.Logo == null)
            {
                throw new UserFriendlyException((int)HttpStatusCode.BadRequest, $"The team logo is required.");
            }
            if (Repository.Count(t=>t.Name.ToLower() == input.Name.ToLower()) > 0)
            {
                throw new UserFriendlyException((int)HttpStatusCode.BadRequest, $"The team name '{input.Name}' already exists.");
            }

            var teamId = Guid.NewGuid().ToString();
            try
            {
                // validate input
                Convert.FromBase64String(input.Logo);
                _teamLogoRepository.Insert(new TeamLogo
                {
                    Id = Guid.NewGuid().ToString(),
                    TeamId = teamId,
                    Base64 = input.Logo
                });
            }
            catch(Exception ex)
            {
                throw new UserFriendlyException((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            input.CreatorId = AbpSession.UserId.Value;
            input.Id = teamId;
            
            return base.CreateAsync(input);
        }

        public override async Task<TeamDto> UpdateAsync(UpdateTeamDto input)
        {
            if (input.Logo.IsNullOrEmpty()) return await base.UpdateAsync(input);

            try
            {
                Convert.FromBase64String(input.Logo);
                var logo = await _teamLogoRepository.FirstOrDefaultAsync(tl => tl.TeamId == input.Id);
                if (logo == null)
                {
                    await _teamLogoRepository.InsertAsync(new TeamLogo
                    {
                        Id = Guid.NewGuid().ToString(),
                        TeamId = input.Id,
                        Base64 = input.Logo
                    });
                }
                else
                {
                    logo.Base64 = input.Logo;
                    await _teamLogoRepository.UpdateAsync(logo);
                }
                return await base.UpdateAsync(input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<PagedResultDto<TeamDto>> Filter(FilterTeamDto input)
        {
            if (string.IsNullOrEmpty(input.Name))
            {
                var result = await base.GetAllAsync(input);
                return result;
            }
            var teamsQuery = Repository.GetAll().Where(a => a.Name.ToLower().Contains(input.Name.ToLower()));
            var teams = teamsQuery.Select(art => new TeamDto
            {
                CreatorId = art.CreatorId,
                Name = art.Name,
                Description = art.Description,
            }).OrderBy(art => art.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var totalCount = teamsQuery.Count();
            return new PagedResultDto<TeamDto>(totalCount, teams);
        }

        public async Task<string> TeamLogo(EntityDto<string> entityDto)
        {
            var logo = await _teamLogoRepository.FirstOrDefaultAsync(tl => tl.TeamId == entityDto.Id);
            if (logo == null)
            {
                throw new UserFriendlyException("Logo not found");
            }

            return logo.Base64;
        }
        public async Task<PagedResultDto<TeamDto>> MyTeams(PagedResultRequestDto input)
        {
            var query = from t in Repository.GetAll()
                join tm in _teamMemberRepository.GetAll() on t.Id equals tm.TeamId
                where tm.UserId == AbpSession.UserId.Value
                select t;

            var totalCount = await query.CountAsync();
            var teams = query.OrderBy(t => t.Id)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .Select(art =>new TeamDto
            {
                Name = art.Name,
                Description = art.Description,
                CreatorId = art.CreatorId
            }).ToList();
            return new PagedResultDto<TeamDto>(totalCount, teams);
        }

        public async Task<PagedResultDto<TeamMemberInfo>> GetTeamMembers(FetchTeamDto input)
        {
            var query = (from tm in _teamMemberRepository.GetAll()
                join u in _userRepository.GetAll() on tm.UserId equals u.Id
                orderby tm.Id
                where tm.TeamId == input.TeamId
                select new TeamMemberInfo
                {
                    Role = tm.Role, 
                    Username = u.UserName, 
                    DisplayName = u.FullName, 
                    UserId = u.Id
                });

            var totalCount = await query.CountAsync();
            var members = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            
            return new PagedResultDto<TeamMemberInfo>(totalCount, members);
        }

        public async Task<TeamMemberDto> AddTeamMember(CreateTeamMemberDto input)
        {
            try
            {
                var team = await Repository.FirstOrDefaultAsync(t => t.Id == input.TeamId);
                if (team.CreatorId != AbpSession.UserId.Value)
                {
                    var currentMember = await _teamMemberRepository.FirstOrDefaultAsync(tm => tm.UserId == AbpSession.UserId.Value
                        && tm.TeamId == input.TeamId && tm.Role == TeamRole.Admin);
                    if (currentMember == null)
                    {
                        throw new UserFriendlyException(
                            "Access Denied; you are neither the owner nor an admin of this team");
                    }
                }
                var existingRecord = _teamMemberRepository.GetAll().FirstOrDefault(a => a.UserId == input.UserId && a.TeamId == input.TeamId);
                if (existingRecord != null)
                {
                    throw new UserFriendlyException((int)HttpStatusCode.BadRequest, $"The member is already in this team.");
                }

                var tm = ObjectMapper.Map<TeamMember>(input);
                tm.Id = Guid.NewGuid().ToString();
                var teamMember = await _teamMemberRepository.InsertAsync(tm);
                return ObjectMapper.Map<TeamMemberDto>(teamMember);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> ChangeRole(ChangeRoleInput input)
        {
            var teamMember = await _teamMemberRepository.FirstOrDefaultAsync(tm => tm.Id == input.TeamMemberId);
            if ((await Repository.CountAsync(t => t.Id == teamMember.TeamId 
                                                  && t.CreatorId == AbpSession.UserId.Value)) == 0)
            {
                throw new UserFriendlyException("You are not the creator of this team");
            }
            teamMember.Role = input.Role;
            await _teamMemberRepository.UpdateAsync(teamMember);
            return true;
        }

        public async Task<TeamMemberDto> RemoveTeamMember(UpdateTeamMemberDto input)
        {
            var team = await Repository.FirstOrDefaultAsync(t => t.Id == input.TeamId);
            if (team.CreatorId != AbpSession.UserId.Value)
            {
                var currentMember = await _teamMemberRepository.FirstOrDefaultAsync(tm => tm.UserId == AbpSession.UserId.Value
                    && tm.TeamId == input.TeamId && tm.Role == TeamRole.Admin);
                if (currentMember == null)
                {
                    throw new UserFriendlyException(
                        "Access Denied; you are neither the owner nor an admin of this team");
                }
            }
            var oldRecord = _teamMemberRepository.GetAll().FirstOrDefault(a => a.UserId == input.UserId && a.TeamId == input.TeamId);
            if (oldRecord == null)
            {
                throw new UserFriendlyException((int)HttpStatusCode.NotFound, $"The team member not found.");
            }
            await _teamMemberRepository.DeleteAsync(oldRecord);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<TeamMemberDto>(oldRecord);
        }
        
    }
}
