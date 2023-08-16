using Advertisement.Dto.DTOs.ProvidedAdvertisementDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.ValidationRules.FluentValidations
{
    public class ProvidedAdvertisementUpdateDtoValidator:AbstractValidator<ProvidedAdvertisementUpdateDto>
    {
        public ProvidedAdvertisementUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x=>x.Title).NotEmpty();
            RuleFor(x=>x.Description).NotEmpty();
        }
    }
}
