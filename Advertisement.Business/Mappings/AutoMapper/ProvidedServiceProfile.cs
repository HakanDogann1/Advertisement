using Advertisement.Dto.DTOs.ProvidedServiceDtos;
using Advertisement.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.Mappings.AutoMapper
{
    public class ProvidedServiceProfile:Profile
    {
        public ProvidedServiceProfile()
        {
            CreateMap<ProvidedService,ProvidedServiceCreateDto>().ReverseMap();
            CreateMap<ProvidedService,ProvidedServiceUpdateDto>().ReverseMap();
            CreateMap<ProvidedService,ProvidedServiceListDto>().ReverseMap();
        }
    }
}
