using Adivertisement.Common;
using Advertisement.Business.Interfaces;
using Advertisement.DataAccess.UnitOfWork;
using Advertisement.Dto.DTOs.ProvidedAdvertisementDtos;
using Advertisement.Dto.DTOs.ProvidedServiceDtos;
using Advertisement.Entities;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.Services
{
    public class ProvidedAdvertisementService : Service<ProvidedAdvertisementCreateDto, ProvidedAdvertisementUpdateDto, ProvidedAdvertisementListDto, ProvidedAdvertisement>, IProvidedAdvertisementService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public ProvidedAdvertisementService(IMapper mapper, IValidator<ProvidedAdvertisementCreateDto> createDtoValidator, IValidator<ProvidedAdvertisementUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _uow = uow;
            _mapper= mapper;
        }

        public async Task<IResponse<List<ProvidedAdvertisementListDto>>> GetActivatesAsync()
        {
            var data = _uow.GetRepository<ProvidedAdvertisement>().GetAllAsync();
            var dto = _mapper.Map<List<ProvidedAdvertisementListDto>>(data);
            return  new Response<List<ProvidedAdvertisementListDto>>(ResponseType.Success, dto);
        }
    }
}
