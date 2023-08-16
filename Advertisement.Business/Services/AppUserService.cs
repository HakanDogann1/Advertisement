using Adivertisement.Common;
using Advertisement.Business.Extension;
using Advertisement.Business.Interfaces;
using Advertisement.DataAccess.Repositories;
using Advertisement.DataAccess.UnitOfWork;
using Advertisement.Dto.DTOs.AppRoleDtos;
using Advertisement.Dto.DTOs.AppUserDtos;
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
    public class AppUserService : Service<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>, IAppUserService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IValidator<AppUserCreateDto> _createDtoValidator;
        private readonly IValidator<AppUserLoginDto> _loginDtoValidator;
        public AppUserService(IMapper mapper, IValidator<AppUserCreateDto> createDtoValidator, IValidator<AppUserUpdateDto> updateDtoValidator, IUow uow, IValidator<AppUserLoginDto> loginDtoValidator) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _mapper = mapper;
            _uow = uow;
            _createDtoValidator = createDtoValidator;
            _loginDtoValidator = loginDtoValidator;
        }
        public async  Task<IResponse<AppUserCreateDto>> CreateWithRoleAsync(AppUserCreateDto dto,int roleId)
        {
            var validationResult = _createDtoValidator.Validate(dto);
            if(validationResult.IsValid)
            {
                //1. Yol
                var user = _mapper.Map<AppUser>(dto);
                //user.AppUserRoles = new List<AppUserRole>();
                //user.AppUserRoles.Add(new AppUserRole
                //{
                //    AppUser = user,
                //    AppRoleId = roleId
                //});
                await _uow.GetRepository<AppUser>().CreateAsync(user);
                //await _uow.GetRepository<AppUserRole>().CreateAsync(new AppUserRole
                //{
                //    AppRoleId = roleId,
                //    AppUserId=dto.
                //});
                //2. Yol
                await _uow.GetRepository<AppUserRole>().CreateAsync(new AppUserRole
                {
                    AppRoleId = roleId,
                    AppUser = user
                });
                await _uow.SaveChangesAsync();

                return new Response<AppUserCreateDto>(ResponseType.Success,dto);
            }
            return new Response<AppUserCreateDto>(dto,validationResult.ConvertToCustomValidationError());
        }
        public async Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLoginDto loginDto)
        {
            var validationResult = _loginDtoValidator.Validate(loginDto);
            if( validationResult.IsValid)
            {
               var user = await _uow.GetRepository<AppUser>().GetByFilterAsync(x=>x.UserName==loginDto.UserName && x.Password==loginDto.Password);
                if(user != null)
                {
                    var response = _mapper.Map<AppUserListDto>(user);
                    return new Response<AppUserListDto>(ResponseType.Success, response);
                }
                return new Response<AppUserListDto>(ResponseType.NotFound, "Kullanıcı adı veya Şifre yanlış");
            }
            return new Response<AppUserListDto>(ResponseType.ValidationError,"Kullanıcı Adı veya Şifre alanı boş olamaz");
        }
        public async Task<IResponse<List<AppRoleListDto>>> GetRolesByUserIdAsync(int userId)
        {
            var roles = await _uow.GetRepository<AppRole>().GetAllAsync(x=>x.AppUserRoles.Any(x=>x.AppUserId==userId));
            if(roles == null)
            {
                return new Response<List<AppRoleListDto>>(ResponseType.NotFound,"İlgili rol bulunamadı");
            }
            var dto = _mapper.Map<List<AppRoleListDto>>(roles);
            return new Response<List<AppRoleListDto>>(ResponseType.Success, dto);
        } 
    }
}
