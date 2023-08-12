using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Entities
{
    public class AdvertisementAppUser:BaseEntity
    {
        public int ProvidedAdvertisementId { get; set; }
        public ProvidedAdvertisement ProvidedAdvertisement { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int AdvertisementAppUserStatusId { get; set; }
        public AdvertisementAppUserStatus AdvertisementAppUserStatus { get; set; }
        public int MilitaryStatusId { get; set; }
        public MilitaryStatus MilitaryStatus { get; set; }
        public DateTime EndDateTime { get; set; }
        public int WorkExperience { get; set; }
        public string CvPath { get; set; }
    }
}
