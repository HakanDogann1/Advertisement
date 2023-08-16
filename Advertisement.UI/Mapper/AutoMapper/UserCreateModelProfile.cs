using Advertisement.Dto.DTOs.AppUserDtos;
using Advertisement.UI.Models;
using AutoMapper;

namespace Advertisement.UI.Mapper.AutoMapper
{
    public class UserCreateModelProfile:Profile
    {
        public UserCreateModelProfile()
        {
            CreateMap<UserCreateModel,AppUserCreateDto>().ReverseMap();
        }
    }
}
