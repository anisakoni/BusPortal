using BusPortal.BLL.Mapping;
using BusPortal.BLL.Services.Interfaces;
using BusPortal.BLL.Services.Scoped;
using BusPortal.Common.Models;
using BusPortal.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusPortal.BLL.Services;

public static class Startup
{
    public static void RegisterBLLServices(this IServiceCollection services, IConfiguration config)
    {
        services.RegisterDALServices(config);
        services.AddScoped<ILinesService, LinesService>();
        services.AddScoped<IBookingServices, BookingServices>();
        services.AddScoped<IClientService, ClientService>();

        //services.AddScoped<ILoginServices, LoginServices>();


    }
    public static void ConfigureServices(IServiceCollection services)
    {
       
        services.AddAutoMapper(typeof(MappingProfile));
    }


}