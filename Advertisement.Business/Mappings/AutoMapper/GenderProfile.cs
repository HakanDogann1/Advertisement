using Advertisement.Dto.DTOs.GenderDtos;
using Advertisement.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.Mappings.AutoMapper
{
    public class GenderProfile:Profile
    {
        public GenderProfile()
        {
            CreateMap<GenderCreateDto,Gender>().ReverseMap();
            CreateMap<GenderUpdateDto,Gender>().ReverseMap();
            CreateMap<GenderListDto,Gender>().ReverseMap();
        }
    }
}
