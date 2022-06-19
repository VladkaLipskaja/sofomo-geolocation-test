using Microsoft.EntityFrameworkCore;
using Sofomo.Entities;
using Sofomo.Models;
using Sofomo.Services;

namespace Sofomo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);
            ConfigureHttpClient(services);
            ConfigureCustomServices(services);
            
            services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app)
        {
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseRouting();
            app.UseEndpoints(x => x.MapControllers());
        }

        private void ConfigureCustomServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapProfile));
            services.AddScoped<IGeolocationHelper, GeolocationHelper>();
            services.AddScoped<IGeolocationService, GeolocationService>();
            services.AddScoped<IRepository<GeolocationEntity>, Repository<GeolocationEntity>>();
        }

        private const string DefaultConnectionKey = "DefaultConnection";
        
        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<GeolocationDBContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString(DefaultConnectionKey)));
        }

        private void ConfigureHttpClient(IServiceCollection services)
        {
            var ipStackOptions = new IPStackOptions();
            Configuration.GetSection(nameof(IPStackOptions)).Bind(ipStackOptions);
            
            services.AddOptions<IPStackOptions>().Bind(Configuration.GetSection(nameof(IPStackOptions)));
            
            services.AddHttpClient(ipStackOptions.ClientName, c =>
            {
                c.BaseAddress = new Uri(ipStackOptions.Address);
            });
        }
    }
}