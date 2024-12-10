using Microsoft.AspNetCore.Mvc.Rendering;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.ViewModels
{
    public class PrintingLogViewModel
    {
        public Printers Printer { get; set; }
        public UploadFile UploadFile { get; set; }
        public List<SelectListItem> PaperSizes { get; set; } = new List<SelectListItem>();

        public PrintingLogs PrintingLog { get; set; }
    }
}
