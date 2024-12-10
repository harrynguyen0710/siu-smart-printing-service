using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using siu_smart_printing_service.Areas.Admin.Services;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FileTypeController : Controller
    {
        private readonly FileTypeService _fileTypeService;
        public FileTypeController(FileTypeService fileTypeService)
        {
            _fileTypeService = fileTypeService;
        }
        public async Task<IActionResult> Index()
        {
            var fileTypes = await _fileTypeService.GetAllAsync();
            return View(fileTypes);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(FileTypes type)
        {
            await _fileTypeService.Add(type);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var fileType = await _fileTypeService.GetById(id);
            return View(fileType);  
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FileTypes type)
        {
            Console.WriteLine($"id:: {type.id}");
            Console.WriteLine($"name:: {type.name}");
            Console.WriteLine($"description:: {type.description}");

            await _fileTypeService.Edit(type);
            return RedirectToAction("Index", "Configuration");
        }
    }
}
