﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using Legalpedia.Authorization;
using Legalpedia.Authorization.Accounts;
using Legalpedia.Authorization.Roles;
using Legalpedia.Authorization.Users;
using Legalpedia.Packages.Dto;
using Legalpedia.Roles.Dto;
using Legalpedia.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Legalpedia.Users
{
    [AbpAuthorize(PermissionNames.PagesAdminUsers)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long,
        PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly IRepository<UserPicture, string> _pictureRepository;
        private readonly IRepository<User, long> _userRepository;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager, 
            IRepository<UserPicture, string> pictureRepository, 
            IRepository<User, long> userRepository)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _pictureRepository = pictureRepository;
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetProfile(EntityDto<string> input)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.UserName == input.Id);
            return ObjectMapper.Map<UserDto>(user);
        }
        
        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.Id = await _userRepository.CountAsync() + 1;
            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        public async Task<UserDto> UpdateBio(UpdateBioInput input)
        {
            if (AbpSession.UserId == null)
            {
                throw new UserFriendlyException("Please login to update your profile");
            }
            var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
            user.Bio = input.Bio;
            CheckErrors(await _userManager.UpdateAsync(user));
            return ObjectMapper.Map<UserDto>(user);
        }
        

        public async Task<UserDto> UpdateDetail(UserDetail input) {
            if (AbpSession.UserId == null)
            {
                throw new UserFriendlyException("Please login to update your profile");
            }
            var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
            user.Bio = input.Bio;
            user.Instagram = input.Instagram;
            user.Twitter = input.Twitter;
            user.Facebook = input.Facebook;
            user.Website = input.Website;
            user.Linedin = input.Linedin;
            user.CallToBarYear = input.CallToBarYear;
            CheckErrors(await _userManager.UpdateAsync(user));
            return ObjectMapper.Map<UserDto>(user);
        }

        public async Task<bool> ChangeProfilePicture(ChangePictureInput input)
        {
            try 
            {
                var picture = await _pictureRepository.FirstOrDefaultAsync(p => p.UserId == _abpSession.UserId.Value);
                if (picture == null)
                {
                    await _pictureRepository.InsertAsync(new UserPicture
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = AbpSession.UserId.Value,
                        Base64 = input.Image
                    });
                    return true;
                }

                picture.Base64 = input.Image;
                await _pictureRepository.UpdateAsync(picture);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<bool> ChangePicture(string input)
        {
            try 
            {
                var picture = await _pictureRepository.FirstOrDefaultAsync(p => p.UserId == _abpSession.UserId.Value);
                if (picture == null)
                {
                    await _pictureRepository.InsertAsync(new UserPicture
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = AbpSession.UserId.Value,
                        Base64 = input
                    });
                    return true;
                }

                picture.Base64 = input;
                await _pictureRepository.UpdateAsync(picture);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<UserPicture> MyProfilePicture()
        {
            return await ProfilePicture(AbpSession.UserId.Value);
        }
        public async Task<UserPicture> ProfilePicture(long userId)
        {
            var picture = await _pictureRepository.FirstOrDefaultAsync(p => p.UserId == userId) ?? new UserPicture();

            return picture;
        }
        
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        [AbpAuthorize(PermissionNames.PagesUsersActivation)]
        public async Task Activate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = true;
            });
        }

        [AbpAuthorize(PermissionNames.PagesUsersActivation)]
        public async Task DeActivate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = false;
            });
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attempting to change password.");
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
            }
            if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
            {
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attempting to reset password.");
            }
            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                CurrentUnitOfWork.SaveChanges();
            }

            return true;
        }
    }
}

