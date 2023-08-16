using Advertisement.Business.Interfaces;
using Advertisement.Dto.DTOs.ProvidedAdvertisementDtos;
using Advertisement.UI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.UI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ApplicationController : Controller
    {
        private readonly IProvidedAdvertisementService _advertisementService;

        public ApplicationController(IProvidedAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        public async Task<IActionResult> List()
        {
            var response = await _advertisementService.GetAllAsync();
            return this.ResponseView(response);
        }

        public IActionResult Create()
        {
            return View(new ProvidedAdvertisementCreateDto());
        }

        [HttpPost]

        public async Task<IActionResult> Create(ProvidedAdvertisementCreateDto dto)
        {
            var response = await _advertisementService.CreateAsync(dto);
            return this.ResponseRedirectAction(response, "List");
        }

        public async Task<IActionResult> Update(int id)
        {
            var response = await _advertisementService.GetByIdAsync<ProvidedAdvertisementUpdateDto>(id);
            return this.ResponseView(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProvidedAdvertisementUpdateDto dto)
        {
            var response = await _advertisementService.UpdateAsync(dto);
            return this.ResponseRedirectAction(response, "List");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var response = await _advertisementService.Remove(id);
            return this.ResponseRedirectAction(response, "List");
        }
    }
}
