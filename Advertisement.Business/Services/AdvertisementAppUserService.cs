using Adivertisement.Common;
using Adivertisement.Common.Enums;
using Advertisement.Business.Extension;
using Advertisement.Business.Interfaces;
using Advertisement.DataAccess.Contexts;
using Advertisement.DataAccess.UnitOfWork;
using Advertisement.Dto.DTOs.AdvertisementAppUserDtos;
using Advertisement.Dto.DTOs.AdvertisementAppUserStatusDtos;
using Advertisement.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.Services
{
    public class AdvertisementAppUserService:IAdvertisementAppUserService
    {
        private readonly AdvertisementContext _context;
        private readonly IUow _uow;
        private readonly IValidator<AdvertisementAppUserCreateDto> _createDtoValidator;
        private readonly IMapper _mapper;

        public AdvertisementAppUserService(IUow uow, IValidator<AdvertisementAppUserCreateDto> createDtoValidator, IMapper mapper, AdvertisementContext context)
        {
            _uow = uow;
            _createDtoValidator = createDtoValidator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IResponse<AdvertisementAppUserCreateDto>> CreateAsync(AdvertisementAppUserCreateDto dto)
        {
            var result = _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var control = await _uow.GetRepository<AdvertisementAppUser>().GetByFilterAsync(x => x.AppUserId == dto.AppUserId && x.ProvidedAdvertisementId == dto.ProvidedAdvertisementId);
                if (control == null)
                {
                    var createdAdvertisementAppUser = _mapper.Map<AdvertisementAppUser>(dto);
                    await _uow.GetRepository<AdvertisementAppUser>().CreateAsync(createdAdvertisementAppUser);
                    await _uow.SaveChangesAsync();
                    return new Response<AdvertisementAppUserCreateDto>(ResponseType.Success, dto);
                }
                List<CustomValidationError> customErrors = new List<CustomValidationError> { new CustomValidationError { ErrorMessage = "Tekrar başvuru yapılamaz.", PropertyName = "" } };
                return new Response<AdvertisementAppUserCreateDto>(dto, customErrors);

            }
            return new Response<AdvertisementAppUserCreateDto>(dto, result.ConvertToCustomValidationError());
        }
        public async Task<List<AdvertisementAppUserListDto>> GetList(AdvertisementAppUserStatusType type)
        {
            //var query = _uow.GetRepository<AdvertisementAppUser>().GetQuery();
            //var list = query.Include(x => x.ProvidedAdvertisement).Include(x => x.AppUser).ThenInclude(x => x.Gender).Include(x => x.AdvertisementAppUserStatus).Include(x => x.MilitaryStatus).Where(x=>x.AdvertisementAppUserStatusId==(int)type).ToListAsync();
            //return _mapper.Map<List<AdvertisementAppUserListDto>>(list);

            var list = _context.AdvertisementAppUsers.Include(x => x.ProvidedAdvertisement).Include(x => x.AppUser).ThenInclude(x => x.Gender).Include(x => x.AdvertisementAppUserStatus).Include(x => x.MilitaryStatus).Where(x => x.AdvertisementAppUserStatusId == (int)type);
            return _mapper.Map<List<AdvertisementAppUserListDto>>(list);
        }

        public async Task SetStatusAsync(int advertisementAppUserId, AdvertisementAppUserStatusType type)
        {
            var query = _uow.GetRepository<AdvertisementAppUser>().GetQuery();

            var entity = await query.SingleOrDefaultAsync(x => x.Id == advertisementAppUserId);
            entity.AdvertisementAppUserStatusId = (int)type;
            await _uow.SaveChangesAsync();
        }
    }
}
