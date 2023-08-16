using Advertisement.Dto.DTOs.AppUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.ValidationRules.FluentValidations
{
    public class AppUserLoginDtoValidator:AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı boş olamaz...");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş olamaz...");
        }
    }
}
