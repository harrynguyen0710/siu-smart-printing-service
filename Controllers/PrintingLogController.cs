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
        private readonly UserService _userService;
        public PrintingLogController(PrinterService printerService, UploadedFileService uploadFileService, 
            UserManager<Users> userManager, PrintingLogService printingLogService, FileTypeService fileTypeService, UserService userService) 
        {
            _printerService = printerService;
            _uploadedFileService = uploadFileService;
            _userManager = userManager;
            _printingLogService = printingLogService;
            _fileTypeService = fileTypeService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var printingLogs = await _printingLogService.GetAllPrintingLogsByUserId(user.Id);
            var userPrintingLogs = new UserPrintingLog
            {
                User = user,
                PrintingLogs = printingLogs,
            };
            return View(userPrintingLogs);
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

            var remaininBalance = user.balance - model.PrintingLog.numberOfCopies;
            if (remaininBalance < 0)
            {
                TempData["InvalidBalance"] = "You do not enough page for printing!";
                return RedirectToAction("PrintDocument", new { printerId = printer.printerId });
            }


            model.PrintingLog.status = "Successful";
            model.PrintingLog.printerId = model.Printer.printerId;
            model.PrintingLog.startDate = DateTime.Now;
            model.PrintingLog.endDate = DateTime.Now;
            model.PrintingLog.uploadFileId = model.UploadFile.id;

            printer.paperCount = remainingPages;
            user.balance = remaininBalance;

            await _printerService.EditPrinter(printer);
            await _userService.Update(user);
            await _printingLogService.PrintDocument(model.PrintingLog);


            TempData["SuccessMessage"] = "Print job completed successfully!";
            return RedirectToAction("Index","Printer");
        }


    }
}
