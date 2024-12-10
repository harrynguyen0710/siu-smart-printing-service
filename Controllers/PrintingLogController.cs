using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using siu_smart_printing_service.Areas.Admin.Services;
using siu_smart_printing_service.Enum;
using siu_smart_printing_service.Models;
using siu_smart_printing_service.Services;
using siu_smart_printing_service.ViewModels;

namespace siu_smart_printing_service.Controllers
{
    [Authorize]
    public class PrintingLogController : Controller
    {
        private readonly PrinterService _printerService;
        private readonly UploadedFileService _uploadedFileService;
        private readonly UserManager<Users> _userManager;
        private readonly PrintingLogService _printingLogService;
        private readonly FileTypeService _fileTypeService;
        public PrintingLogController(PrinterService printerService, UploadedFileService uploadFileService, 
            UserManager<Users> userManager, PrintingLogService printingLogService, FileTypeService fileTypeService) 
        {
            _printerService = printerService;
            _uploadedFileService = uploadFileService;
            _userManager = userManager;
            _printingLogService = printingLogService;
            _fileTypeService = fileTypeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PrintDocument(int printerId)
        {
            var user = await _userManager.GetUserAsync(User);
            var printer = await _printerService.GetById(printerId);
            var files = await _uploadedFileService.GetAllFileByUserId(user.Id);
            ViewBag.Files = new SelectList(files, "id", "fileName");
            var paperSizes = System.Enum.GetValues(typeof(PaperSize))
                .Cast<PaperSize>()
                .Select(ps => new SelectListItem
                {
                    Value = ps.ToString(),
                    Text = ps.GetDisplayName()
                })
                .ToList();


            var printingLogs = new PrintingLogViewModel()
            {
                Printer = printer,
                PaperSizes = paperSizes
            };

            return View(printingLogs);
        }

        [HttpPost]
        public async Task<IActionResult> PrintDocument(PrintingLogViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var uploadFile = await _uploadedFileService.GetById(model.UploadFile.id);
            var accecptedFiles = await _fileTypeService.GetAllAcceptedFileTypes();
            var acceptedFileTypeId = accecptedFiles.Select(ft => ft.id).ToList();
            var printer = await _printerService.GetById(model.Printer.printerId);


            if (!acceptedFileTypeId.Contains(uploadFile.fileTypes.id)) 
            {
               
                TempData["InvalidType"] = "Can not print unaccepted file type!";
                return RedirectToAction("PrintDocument", new { printerId = printer.printerId });
            }

            var remainingPages = printer.paperCount - model.PrintingLog.numberOfCopies;
            if (remainingPages < 0) 
            {
                TempData["InvalidQuatity"] = "Not enough page for printing!";
                return RedirectToAction("PrintDocument", new { printerId = printer.printerId });
            }



            model.PrintingLog.printerId = model.Printer.printerId;
            model.PrintingLog.startDate = DateTime.Now;
            model.PrintingLog.endDate = DateTime.Now;
            model.PrintingLog.uploadFileId = model.UploadFile.id;

            printer.paperCount = remainingPages;

            await _printingLogService.PrintDocument(model.PrintingLog);


            TempData["SuccessMessage"] = "Print job completed successfully!";
            return RedirectToAction("Index","Printer");
        }


    }
}
