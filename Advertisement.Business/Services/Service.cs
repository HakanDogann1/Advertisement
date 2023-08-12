using Adivertisement.Common;
using Advertisement.Business.Extension;
using Advertisement.Business.Interfaces;
using Advertisement.DataAccess.Repositories;
using Advertisement.DataAccess.UnitOfWork;
using Advertisement.Dto.DTOs.Abstract;
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
    public class Service<CreateDto, UpdateDto, ListDto, T> : IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDtoValidator;
        private readonly IValidator<UpdateDto> _updateDtoValidator;
        private readonly IValidator<ListDto> _listDtoValidator;
        private readonly IUow _uow;

        public Service(IMapper mapper, IValidator<CreateDto> createDtoValidator, IValidator<UpdateDto> updateDtoValidator, IValidator<ListDto> listDtoValidator, IUow uow)
        {
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _listDtoValidator = listDtoValidator;
            _uow = uow;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto createDto)
        {
            var result = _createDtoValidator.Validate(createDto);
            if (result.IsValid)
            {
                var createdDto = _mapper.Map<T>(createDto);
                await _uow.GetRepository<T>().CreateAsync(createdDto);
                return new Response<CreateDto>(ResponseType.Success, createDto);
            }
            return new Response<CreateDto>(createDto, result.ConvertToCustomValidationError());

        }

        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto updateDto)
        {
            var result = _updateDtoValidator.Validate(updateDto);
            if (result.IsValid)
            {
                var updatedData = await _uow.GetRepository<T>().FindAsync(updateDto.Id);
                if (updatedData == null)
                {
                    return new Response<UpdateDto>(ResponseType.NotFound, $"{updateDto.Id} sine ait veri bulunamadı");
                }
                var entity = _mapper.Map<T>(updatedData);
                _uow.GetRepository<T>().Update(entity, updatedData);
                return new Response<UpdateDto>(ResponseType.Success, updateDto);
            }
            return new Response<UpdateDto>(updateDto,result.ConvertToCustomValidationError());

        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<T>().GetAllAsync();
            var dto = _mapper.Map<List<ListDto>>(data);
            return new Response<List<ListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = await _uow.GetRepository<T>().FindAsync(id);
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ye ait bir data bulunamadı.");
            }
            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> Remove(int id)
        {
            var data = await _uow.GetRepository<T>().FindAsync(id);
            if (data == null)
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait veri bulunamadı");
            }
            _uow.GetRepository<T>().Remove(data);
            return new Response(ResponseType.Success);
        }
    }
}
