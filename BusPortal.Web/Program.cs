using BusPortal.BLL.Services;
using BusPortal.DAL;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using BusPortal.BLL.Mapping;
using BusPortal.DAL.Persistence.Entities;
using BusPortal.BLL.Services.Interfaces;
using BusPortal.BLL.Services.Scoped;
using BusPortal.DAL.Persistence.Repositories;
using BusPortal.Web.Controllers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<AccountController>();

builder.Services.AddControllersWithViews();


builder.Services.RegisterBLLServices(builder.Configuration);
builder.Services.RegisterDALServices(builder.Configuration);


builder.Services.AddIdentity<BusPortal.DAL.Persistence.Entities.ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BusPortal.DAL.Persistence.DALDbContext>()
    .AddDefaultTokenProviders();

 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddScoped<IPasswordHasher<Client>, PasswordHasher<Client>>();
//builder.Services.AddScoped<IClientService, ClientService>();
//builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<ILinesService, LinesService>();
//builder.Services.AddScoped(typeof(_IBaseRepository<,>), typeof(_IBaseRepository<,>));
builder.Services.AddScoped<IPasswordHasher<Client>, PasswordHasher<Client>>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ILinesService, LinesService>();
//builder.Services.AddScoped(typeof(_IBaseRepository<,>), typeof(_BaseRepository<,>));



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); 
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
