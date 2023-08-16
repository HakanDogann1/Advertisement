using Adivertisement.Common.Enums;
using Advertisement.Business.Interfaces;
using Advertisement.Business.Services;
using Advertisement.Dto.DTOs.AdvertisementAppUserDtos;
using Advertisement.Dto.DTOs.AppUserDtos;
using Advertisement.Dto.DTOs.MilitaryStatusDtos;
using Advertisement.UI.Extensions;
using Advertisement.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace Advertisement.UI.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAppUserService _userService;
        private readonly IAdvertisementAppUserService _advertisementAppUserService;

        public AdvertisementController(IAppUserService userService, IAdvertisementAppUserService advertisementAppUserService)
        {
            _userService = userService;
            _advertisementAppUserService = advertisementAppUserService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Send(int advertisementId)
        {
            var userId = int.Parse((User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)).Value);
            var userResponse = await _userService.GetByIdAsync<AppUserListDto>(userId);
            var user = userResponse.Data;
            ViewBag.genderId = user.GenderId;
            //TempData["genderId"]=user.GenderId;
            var list = new List<MilitaryStatusListDto>();
            var items = Enum.GetValues(typeof(MilitaryStatusType));
            foreach (var item in items)
            {
                list.Add(new MilitaryStatusListDto
                {
                    Id = (int)item,
                    Definition = Enum.GetName(typeof(MilitaryStatusType), item)
                });


            }
            ViewBag.MilitaryStatus = new SelectList(list, "Id", "Definition");
            return View(new AdvertisementAppUserCreateModel
            {
                ProvidedAdvertisementId = advertisementId,
                AppUserId = userId,

            });
        }
        [HttpPost]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Send(AdvertisementAppUserCreateModel model)
        {

            AdvertisementAppUserCreateDto dto = new();
            if (model.CvFile != null)
            {
                var fileName = Guid.NewGuid().ToString();
                var extName = Path.GetExtension(model.CvFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cvFiles", fileName + extName);
                var stream = new FileStream(path, FileMode.Create);
                await model.CvFile.CopyToAsync(stream);
                dto.CvPath = path;
            }

            dto.AdvertisementAppUserStatusId = model.AdvertisementAppUserStatusId;
            dto.ProvidedAdvertisementId = model.ProvidedAdvertisementId;
            dto.AppUserId = model.AppUserId;
            dto.EndDateTime = model.EndDateTime;
            dto.MilitaryStatusId = model.MilitaryStatusId;
            dto.WorkExperience = model.WorkExperience;

            var response = await _advertisementAppUserService.CreateAsync(dto);
            if (response.ResponseType == Adivertisement.Common.ResponseType.ValidationError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                var userId = int.Parse((User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)).Value);
                var userResponse = await _userService.GetByIdAsync<AppUserListDto>(userId);

                ViewBag.GenderId = userResponse.Data.GenderId;

                var items = Enum.GetValues(typeof(MilitaryStatusType));

                var list = new List<MilitaryStatusListDto>();

                foreach (int item in items)
                {
                    list.Add(new MilitaryStatusListDto
                    {
                        Id = item,
                        Definition = Enum.GetName(typeof(MilitaryStatusType), item),
                    });
                }

                ViewBag.MilitaryStatus = new SelectList(list, "Id", "Definition");

                return View(model);
            }
            else
            {
                return RedirectToAction("HumanResource", "Default");
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Başvuru);
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetStatus(int advertisementAppUserId, AdvertisementAppUserStatusType type)
        {
            await _advertisementAppUserService.SetStatusAsync(advertisementAppUserId, type);
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApprovedList()
        {
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Mülakat);
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectedList()
        {
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Olumsuz);
            return View(list);
        }

    }
}
