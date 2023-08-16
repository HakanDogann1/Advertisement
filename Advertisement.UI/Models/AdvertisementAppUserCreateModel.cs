using Adivertisement.Common.Enums;

namespace Advertisement.UI.Models
{
    public class AdvertisementAppUserCreateModel
    {
        public int ProvidedAdvertisementId { get; set; }
        public int AppUserId { get; set; }
        public int AdvertisementAppUserStatusId { get; set; } = (int)AdvertisementAppUserStatusType.Başvuru;
        public int MilitaryStatusId { get; set; }
        public DateTime EndDateTime { get; set; }
        public int WorkExperience { get; set; }
        public IFormFile CvFile { get; set; }

    }
}
