using Advertisement.Dto.DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Dto.DTOs.AppRoleDtos
{
    public class AppRoleListDto:IDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }

    }
}
