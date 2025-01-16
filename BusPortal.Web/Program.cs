using BusPortal.BLL.Services;
using BusPortal.BLL.Services.Interfaces;
using BusPortal.BLL.Services.Scoped;
using BusPortal.Common.Models;
using BusPortal.DAL;
using BusPortal.DAL.Persistence;
using BusPortal.DAL.Persistence.Repositories;
using BusPortal.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.RegisterBLLServices(builder.Configuration);

builder.Services.AddAuthentication()
    .AddCookie(options =>
    {
        options.LoginPath = "/Clients/Login";
        options.AccessDeniedPath = "/Shared/Error";
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
