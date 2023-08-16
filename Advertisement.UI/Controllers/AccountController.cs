using Adivertisement.Common.Enums;
using Advertisement.Business.Interfaces;
using Advertisement.Dto.DTOs.AppUserDtos;
using Advertisement.UI.Extensions;
using Advertisement.UI.Mapper.AutoMapper;
using Advertisement.UI.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading;

namespace Advertisement.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateModel> _userCreateValidator;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public AccountController(IGenderService genderService, IValidator<UserCreateModel> userCreateValidator, IAppUserService appUserService, IMapper mapper)
        {
            _genderService = genderService;
            _userCreateValidator = userCreateValidator;
            _appUserService = appUserService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Signup()
        {
            var response = await _genderService.GetAllAsync();
            var model = new UserCreateModel
            {
                Genders = new SelectList(response.Data, "Id", "Definition")
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel model)
        {
            var result = _userCreateValidator.Validate(model);
            if (result.IsValid)
            {
                //Genelde lookup tablolarda gerçekleşir.
                var value = _mapper.Map<AppUserCreateDto>(model);
                var createResponse = await _appUserService.CreateWithRoleAsync(value, (int)RoleType.Member);
                return this.ResponseRedirectAction(createResponse, "SignIn");

            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            var response = await _genderService.GetAllAsync();
            model.Genders = new SelectList(response.Data, "Id", "Definition", model.GenderId);
            return View(model);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLoginDto dto)
        {

            var result = await _appUserService.CheckUserAsync(dto);
            if (result.ResponseType == Adivertisement.Common.ResponseType.Success)
            {
                var roleResult = await _appUserService.GetRolesByUserIdAsync(result.Data.Id);
                var claims = new List<Claim>();
                if (roleResult.ResponseType == Adivertisement.Common.ResponseType.Success)
                {
                    foreach (var item in roleResult.Data)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.Definition));
                    }
                }
                claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()));

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                    IsPersistent = dto.RememberMe,

                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index","Default");   
            }

            ModelState.AddModelError("Kullanıcı Adı veya Şifre hatalı", result.Message);
            return View(dto);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(
      CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Default");
        }
    }
}
