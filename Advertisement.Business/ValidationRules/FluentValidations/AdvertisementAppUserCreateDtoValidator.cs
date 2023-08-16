using Adivertisement.Common.Enums;
using Advertisement.Dto.DTOs.AdvertisementAppUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.ValidationRules.FluentValidations
{
    public class AdvertisementAppUserCreateDtoValidator:AbstractValidator<AdvertisementAppUserCreateDto>
    {
        public AdvertisementAppUserCreateDtoValidator()
        {
            RuleFor(x => x.AdvertisementAppUserStatusId).NotEmpty();
            RuleFor(x=>x.ProvidedAdvertisementId).NotEmpty();
            RuleFor(x=>x.AppUserId).NotEmpty();
            RuleFor(x=>x.CvPath).NotEmpty().WithMessage("Bir cv dosyası seçiniz");
            RuleFor(x=>x.EndDateTime).NotEmpty().When(x=>x.MilitaryStatusId==(int)MilitaryStatusType.Tecilli).WithMessage("Tecil tarihi boş bırakılamaz.");

        }
    }
}
