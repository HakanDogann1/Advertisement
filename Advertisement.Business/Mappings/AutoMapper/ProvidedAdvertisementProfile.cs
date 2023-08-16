using Advertisement.Dto.DTOs.ProvidedAdvertisementDtos;
using Advertisement.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.Mappings.AutoMapper
{
    public class ProvidedAdvertisementProfile:Profile
    {
        public ProvidedAdvertisementProfile()
        {
            CreateMap<ProvidedAdvertisementCreateDto, ProvidedAdvertisement>().ReverseMap();
            CreateMap<ProvidedAdvertisementUpdateDto, ProvidedAdvertisement>().ReverseMap();
            CreateMap<ProvidedAdvertisementListDto, ProvidedAdvertisement>().ReverseMap();
        }
    }
}
