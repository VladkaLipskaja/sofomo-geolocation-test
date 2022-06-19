using Microsoft.EntityFrameworkCore;

namespace Sofomo.Entities
{
    public class GeolocationDBContext : DbContext
    {
        
        public DbSet<GeolocationEntity> Geolocation { get; set; }

        public GeolocationDBContext(DbContextOptions<GeolocationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.ApplyConfiguration(new GeolocationMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}