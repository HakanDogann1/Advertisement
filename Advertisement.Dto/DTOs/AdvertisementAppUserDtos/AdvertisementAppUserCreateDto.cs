using Advertisement.Dto.DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Dto.DTOs.AdvertisementAppUserDtos
{
    public class AdvertisementAppUserCreateDto:IDto
    {
        public int ProvidedAdvertisementId { get; set; }
        public int AppUserId { get; set; }
        public int AdvertisementAppUserStatusId { get; set; }
        public int MilitaryStatusId { get; set; }
        public DateTime EndDateTime { get; set; }
        public int WorkExperience { get; set; }
        public String CvPath { get; set; }
    }
}
