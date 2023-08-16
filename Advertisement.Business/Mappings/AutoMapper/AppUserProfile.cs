using Advertisement.Dto.DTOs.AppUserDtos;
using Advertisement.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.Mappings.AutoMapper
{
    public class AppUserProfile:Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUserCreateDto,AppUser>().ReverseMap();
            CreateMap<AppUserUpdateDto,AppUser>().ReverseMap();
            CreateMap<AppUserListDto,AppUser>().ReverseMap();
        }
    }
}
