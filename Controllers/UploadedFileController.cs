using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using siu_smart_printing_service.Areas.Admin.Services;
using siu_smart_printing_service.Models;
using siu_smart_printing_service.Services;

namespace siu_smart_printing_service.Controllers
{
    [Authorize]
    public class UploadedFileController : Controller
    {
        private readonly UploadedFileService _uploadedFileService;
        private readonly UserManager<Users> _userManager;
        private readonly FileTypeService _fileTypeService;


        public UploadedFileController(UploadedFileService uploadedFileService, UserManager<Users> userManager, FileTypeService fileTypeService)
        {
            _uploadedFileService = uploadedFileService;
            _userManager = userManager;
            _fileTypeService = fileTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var files = await _uploadedFileService.GetAllFileByUserId(user.Id);
            return View(files);
        }

        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            var fileTypes = await _fileTypeService.GetAllAcceptedFileTypes();
            ViewBag.FileTypes = new SelectList(fileTypes, "id", "name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "Please select a valid file.";
                return RedirectToAction("Upload", "UploadedFile");
            }

            var acceptedFileTypes = await _fileTypeService.GetAllAcceptedFileTypes();
            var acceptedFileTypeNames = acceptedFileTypes.Select(ft => ft.name).ToList();

            if (!acceptedFileTypeNames.Contains(file.ContentType))
            {
                TempData["ErrorMessage"] = "The uploaded file type is not allowed.";
                return RedirectToAction("Upload", "UploadedFile");
            }

            var fileName = Path.GetFileName(file.FileName);
            var fileType = file.ContentType;
            var fileSize = file.Length;

            var type = await _fileTypeService.GetFileTypeByName(fileType);

            var uploadFile = new UploadFile
            {
                fileName = fileName,
                fileTypeId = type.id,
                fileSize = fileSize.ToString(),
                userId = user.Id,
                updatedDate = DateTime.UtcNow,
            };

            await _uploadedFileService.Add(uploadFile);

            var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            TempData["SuccessMessage"] = "File uploaded successfully!";
            return RedirectToAction("Index", "UploadedFile");

        }
    }
}
