using Advertisement.Dto.DTOs.ProvidedServiceDtos;
using Advertisement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.Interfaces
{
    public interface IProvidedServiceService:IService<ProvidedServiceCreateDto,ProvidedServiceUpdateDto,ProvidedServiceListDto,ProvidedService>
    {

    }
}
