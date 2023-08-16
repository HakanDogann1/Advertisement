using Advertisement.Dto.DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Dto.DTOs.AppRoleDtos
{
    public class AppRoleCreateDto:IDto
    {
        public string Definition { get; set; }
    }
}
