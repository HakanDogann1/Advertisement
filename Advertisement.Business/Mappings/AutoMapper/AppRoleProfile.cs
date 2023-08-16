using Advertisement.Dto.DTOs.AppRoleDtos;
using Advertisement.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.Mappings.AutoMapper
{
    public class AppRoleProfile:Profile
    {
        public AppRoleProfile()
        {
            CreateMap<AppRoleCreateDto,AppRole>().ReverseMap();
            CreateMap<AppRoleListDto,AppRole>().ReverseMap();
            CreateMap<AppRoleUpdateDto,AppRole>().ReverseMap();
        }
    }
}
