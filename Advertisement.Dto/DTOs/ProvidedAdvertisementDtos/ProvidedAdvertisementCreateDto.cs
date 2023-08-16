using Advertisement.Dto.DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Dto.DTOs.ProvidedAdvertisementDtos
{
    public class ProvidedAdvertisementCreateDto:IDto
    {
        public string Title { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
    }
}
