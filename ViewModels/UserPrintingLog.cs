using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.ViewModels
{
    public class UserPrintingLog
    {
        public Users User;
        public IEnumerable<PrintingLogs> PrintingLogs;
    }
}
