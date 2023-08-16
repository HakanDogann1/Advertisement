using Advertisement.Dto.DTOs.ProvidedServiceDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Business.ValidationRules.FluentValidations
{
    public class ProvidedServiceListDtoValidator:AbstractValidator<ProvidedServiceListDto>
    {
        public ProvidedServiceListDtoValidator()
        {
            
        }
    }
}
