using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using siu_smart_printing_service.Areas.Admin.Services;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PrinterController : Controller
    {
        private readonly PrinterService _printerService;
        private readonly RoomService _roomService;
        public PrinterController(PrinterService printerService, RoomService roomService)
        {
            _printerService = printerService;
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {
            var printers = await _printerService.GetAllPrinters();
            return View(printers);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var rooms = await _roomService.GetAllAsync();
            ViewBag.Rooms = new SelectList(rooms, "roomId", "roomName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Printers printer)
        {
            await _printerService.AddPrinter(printer);
            TempData["SuccessMessage"] = "Add successful";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int printerId)
        {
            var rooms = await _roomService.GetAllAsync();
            var printer = await _printerService.GetById(printerId);

            ViewBag.Rooms = new SelectList(rooms, "roomId", "roomName");

            return View(printer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Printers printer)
        {

            await _printerService.EditPrinter(printer);

            TempData["SuccessMessage"] = "Edit successful";

            return RedirectToAction("Index");

        }


    }
}
