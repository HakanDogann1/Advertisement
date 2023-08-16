using Adivertisement.Common;
using Advertisement.Dto.DTOs.ProvidedAdvertisementDtos;
using Advertisement.Dto.DTOs.ProvidedServiceDtos;
using Advertisement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.Interfaces
{
    public interface IProvidedAdvertisementService:IService<ProvidedAdvertisementCreateDto,ProvidedAdvertisementUpdateDto,ProvidedAdvertisementListDto,ProvidedAdvertisement>
    {
        Task<IResponse<List<ProvidedAdvertisementListDto>>> GetActivatesAsync();
    }
}
