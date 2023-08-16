using Advertisement.Dto.DTOs.MilitaryStatusDtos;
using Advertisement.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.Mappings.AutoMapper
{
    public class MilitaryStatusProfile:Profile
    {
        public MilitaryStatusProfile()
        {
            CreateMap<MilitaryStatus,MilitaryStatusListDto>().ReverseMap();
        }
    }
}
