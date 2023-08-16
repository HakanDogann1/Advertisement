using Advertisement.Dto.DTOs.ProvidedAdvertisementDtos;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.ValidationRules.FluentValidations
{
    public class ProvidedAdvertisementCreateDtoValidator:AbstractValidator<ProvidedAdvertisementCreateDto>
    {
        public ProvidedAdvertisementCreateDtoValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x=>x.Status).NotEmpty();
        }
    }
}
