using Advertisement.Business.Interfaces;
using Advertisement.UI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.UI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IProvidedServiceService _service;
        private readonly IProvidedAdvertisementService _advertisementService;
        public DefaultController(IProvidedServiceService service, IProvidedAdvertisementService advertisementService)
        {
            _service = service;
            _advertisementService = advertisementService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _service.GetAllAsync();
            return this.ResponseView(response);
        }
        public async Task<IActionResult> HumanResource()
        {
            var response = await _advertisementService.GetAllAsync();
            return this.ResponseView(response);
        }
    }
}
