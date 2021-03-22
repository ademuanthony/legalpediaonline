using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Legalpedia.Authorization.Users;
using Legalpedia.Packages.Dto;
using Legalpedia.Roles.Dto;
using Legalpedia.Users.Dto;

namespace Legalpedia.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<UserDto> GetProfile(EntityDto<string> input);
        Task DeActivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);
        Task<bool> ChangeLogo(ChangeLogoInput input);
        Task<UserPicture> MyProfilePicture();
        Task<UserPicture> ProfilePicture(long userId);
        Task<UserDto> UpdateBio(UpdateBioInput input);
    }
}
