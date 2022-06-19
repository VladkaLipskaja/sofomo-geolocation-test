using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sofomo.Entities
{
    public class GeolocationMap : IEntityTypeConfiguration<GeolocationEntity>
    {
        public void Configure(EntityTypeBuilder<GeolocationEntity> builder)
        {
            // Primary key
            builder.HasKey(t => t.IP);

            // Properties
            builder.ToTable("geolocation");

            builder.Property(t => t.IP).HasColumnName("ip");
            builder.Property(t => t.IPType).HasColumnName("ip_type");
            builder.Property(t => t.ContinentCode).HasColumnName("continent_code");
            builder.Property(t => t.ContinentName).HasColumnName("continent_name");
            builder.Property(t => t.CountryCode).HasColumnName("country_code");
            builder.Property(t => t.CountryName).HasColumnName("country_name");
            builder.Property(t => t.RegionCode).HasColumnName("region_code");
            builder.Property(t => t.RegionName).HasColumnName("region_name");
            builder.Property(t => t.City).HasColumnName("city");
            builder.Property(t => t.Zip).HasColumnName("zip");
            builder.Property(t => t.Latitude).HasColumnName("latitude");
            builder.Property(t => t.Longitude).HasColumnName("longitude");
        }
    }
}