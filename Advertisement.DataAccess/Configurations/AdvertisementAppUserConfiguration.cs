using Advertisement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.DataAccess.Configurations
{
    public class AdvertisementAppUserConfiguration : IEntityTypeConfiguration<AdvertisementAppUser>
    {
        public void Configure(EntityTypeBuilder<AdvertisementAppUser> builder)
        {
            builder.HasIndex(x => new
            {
                x.AppUserId,
                x.ProvidedAdvertisementId
            }).IsUnique();
            builder.Property(x=>x.CvPath).HasMaxLength(500).IsRequired();
            builder.HasOne(x=>x.ProvidedAdvertisement).WithMany(x=>x.AdvertisementAppUsers).HasForeignKey(x=>x.ProvidedAdvertisementId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.AdvertisementAppUsers).HasForeignKey(x => x.AppUserId);
            builder.HasOne(x => x.AdvertisementAppUserStatus).WithMany(x => x.AdvertisementAppUsers).HasForeignKey(x => x.AdvertisementAppUserStatusId);
            builder.HasOne(x => x.MilitaryStatus).WithMany(x => x.AdvertisementAppUsers).HasForeignKey(x => x.MilitaryStatusId);
        }
    }
}
