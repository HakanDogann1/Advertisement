using Advertisement.Dto.DTOs.ProvidedAdvertisementDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.ValidationRules.FluentValidations
{
    public class ProvidedAdvertisementListDtoValidator:AbstractValidator<ProvidedAdvertisementListDto>
    {
        public ProvidedAdvertisementListDtoValidator()
        {
           
        }
    }
}
