using Advertisement.Business.Mappings.AutoMapper;
using Advertisement.Business.ValidationRules.FluentValidations;
using Advertisement.DataAccess.Contexts;
using Advertisement.DataAccess.UnitOfWork;
using Advertisement.Dto.DTOs.ProvidedServiceDtos;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);
var cs = builder.Configuration.GetConnectionString("Local");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AdvertisementContext>(opt =>
{
    opt.UseSqlServer(cs);
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var mapperConfiguration = new MapperConfiguration(opt =>
{
    opt.AddProfile(new ProvidedServiceProfile());
});
builder.Services.AddScoped<IUow, Uow>();
builder.Services.AddTransient<IValidator<ProvidedServiceCreateDto>, ProvidedServiceCreateDtoValidator>();
builder.Services.AddTransient<IValidator<ProvidedServiceUpdateDto>, ProvidedServiceUpdateDtoValidator>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
