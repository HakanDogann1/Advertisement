using Adivertisement.Common;
using Advertisement.Dto.DTOs.Abstract;
using Advertisement.Dto.DTOs.ProvidedServiceDtos;
using Advertisement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.Interfaces
{
    public interface IService<CreateDto, UpdateDto, ListDto,T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity
    {
        Task<IResponse<CreateDto>> CreateAsync(CreateDto createDto);
        Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto updateDto);
        Task<IResponse<IDto>> GetByIdAsync<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<List<ListDto>>> GetAllAsync();
    }
}
