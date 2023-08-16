using Advertisement.Business.Interfaces;
using Advertisement.Business.Mappings.AutoMapper;
using Advertisement.Business.Services;
using Advertisement.Business.ValidationRules.FluentValidations;
using Advertisement.DataAccess.Contexts;
using Advertisement.DataAccess.UnitOfWork;
using Advertisement.Dto.DTOs.AdvertisementAppUserDtos;
using Advertisement.Dto.DTOs.AppUserDtos;
using Advertisement.Dto.DTOs.GenderDtos;
using Advertisement.Dto.DTOs.ProvidedAdvertisementDtos;
using Advertisement.Dto.DTOs.ProvidedServiceDtos;
using Advertisement.UI.Models;
using Advertisement.UI.ValidationRules;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);
var cs = builder.Configuration.GetConnectionString("Local");
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.Cookie.Name = "advertisement";
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.ExpireTimeSpan = TimeSpan.FromDays(20);
        opt.LoginPath = new PathString("/Account/SignIn");
        opt.LogoutPath = new PathString("/Account/LogOut");
        opt.AccessDeniedPath = new PathString("/Account/AccessDenied");
    });
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AdvertisementContext>(opt =>
{
    opt.UseSqlServer(cs);
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IValidator<ProvidedAdvertisementCreateDto>, ProvidedAdvertisementCreateDtoValidator>();
builder.Services.AddTransient<IValidator<ProvidedAdvertisementUpdateDto>, ProvidedAdvertisementUpdateDtoValidator>();
builder.Services.AddTransient<IValidator<ProvidedAdvertisementListDto>, ProvidedAdvertisementListDtoValidator>();
builder.Services.AddTransient<IValidator<ProvidedServiceCreateDto>, ProvidedServiceCreateDtoValidator>();
builder.Services.AddTransient<IValidator<ProvidedServiceUpdateDto>, ProvidedServiceUpdateDtoValidator>();
builder.Services.AddTransient<IValidator<ProvidedServiceListDto>, ProvidedServiceListDtoValidator>();
builder.Services.AddTransient<IValidator<AppUserCreateDto>, AppUserCreateDtoValidator>();
builder.Services.AddTransient<IValidator<AppUserListDto>, AppUserListDtoValidator>();
builder.Services.AddTransient<IValidator<AppUserUpdateDto>, AppUserUpdateDtoValidator>();
builder.Services.AddTransient<IValidator<GenderCreateDto>, GenderCreateDtoValidator>();
builder.Services.AddTransient<IValidator<GenderUpdateDto>, GenderUpdateDtoValidator>();
builder.Services.AddTransient<IValidator<GenderListDto>, GenderListDtoValidator>();
builder.Services.AddTransient<IValidator<UserCreateModel>, UserCreateModelValidator>();
builder.Services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginDtoValidator>();
builder.Services.AddTransient<IValidator<AdvertisementAppUserCreateDto>, AdvertisementAppUserCreateDtoValidator>();

builder.Services.AddScoped<IGenderService, GenderService>();
builder.Services.AddScoped<IProvidedAdvertisementService,ProvidedAdvertisementService>();
builder.Services.AddScoped<IProvidedServiceService, ProvidedServiceService>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IAdvertisementAppUserService, AdvertisementAppUserService>();
//var mapperConfiguration = new MapperConfiguration(opt =>
//{
//    opt.AddProfile(new ProvidedServiceProfile());
//});
builder.Services.AddScoped<IUow, Uow>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
