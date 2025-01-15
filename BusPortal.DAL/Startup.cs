using BusPortal.DAL.Persistence;
using BusPortal.DAL.Persistence.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusPortal.DAL;

public static class Startup
{
    public static void RegisterDALServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DALDbContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("BusPortal"));
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<DALDbContext>()
           .AddDefaultTokenProviders();
        //services.AddScoped<ICarBrandsRepository, CarBrandsRepository>();
    }
}