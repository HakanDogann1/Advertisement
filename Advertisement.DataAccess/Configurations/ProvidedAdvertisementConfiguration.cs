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
    public class ProvidedAdvertisementConfiguration : IEntityTypeConfiguration<ProvidedAdvertisement>
    {
        public void Configure(EntityTypeBuilder<ProvidedAdvertisement> builder)
        {
            builder.Property(x=>x.Title).HasMaxLength(200);
            builder.Property(x=>x.Description).HasColumnType("ntext").IsRequired();
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()");
        }
    }
}
