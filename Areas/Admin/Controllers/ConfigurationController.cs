using Microsoft.AspNetCore.Mvc;
using siu_smart_printing_service.Areas.Admin.Services;
using siu_smart_printing_service.Models;
using siu_smart_printing_service.ViewModels;

namespace siu_smart_printing_service.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ConfigurationController : Controller
    {
        private readonly ConfigurationService _configurationService;
        private readonly FileTypeService _fileTypeService;
        public ConfigurationController(ConfigurationService configurationService, FileTypeService fileTypeService)
        {
            _configurationService = configurationService;
            _fileTypeService = fileTypeService;
        }


        public async Task<IActionResult> Index()
        {
            var configs = await _configurationService.GetAll();
            var fileTypes = await _fileTypeService.GetAllAsync();
            var configViewModel = new ConfigurationViewModel
            {
                FileTypes = fileTypes,
                Configs = configs,
            };
            return View(configViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var config = await _configurationService.GetById(id);
            var fileTypes = await _fileTypeService.GetAllAsync();

            var configModel = new ConfigurationViewModel
            {
                Config = config,
                FileTypes = fileTypes,
            };
            return View(configModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ConfigurationViewModel configurations)
        {
            await _configurationService.Update(configurations.Config);
            TempData["SuccessMessage"] = "Configuration updated successfully!";
            return RedirectToAction("Index");
        }
    }
}
