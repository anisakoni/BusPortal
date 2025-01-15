using BusPortal.DAL.Persistence;
using BusPortal.DAL.Persistence.Entities;
using BusPortal.DAL.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusPortal.DAL
{
    public static class Startup
    {
        public static void RegisterDALServices(this IServiceCollection services, IConfiguration config)
        {
           
            services.AddDbContext<DALDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("BusPortal"));
            });

            // Configure Identity (already included in Program.cs)
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<DALDbContext>()
            //    .AddDefaultTokenProviders();

            
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ILineRepository, LineRepository>();
        }
    }
}
