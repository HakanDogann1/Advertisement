using Adivertisement.Common;
using Advertisement.Dto.DTOs.AppRoleDtos;
using Advertisement.Dto.DTOs.AppUserDtos;
using Advertisement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.Interfaces
{
    public interface IAppUserService : IService<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>
    {
        Task<IResponse<AppUserCreateDto>> CreateWithRoleAsync(AppUserCreateDto dto, int roleId);
        Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLoginDto loginDto);
        Task<IResponse<List<AppRoleListDto>>> GetRolesByUserIdAsync(int userId);


    }
}
