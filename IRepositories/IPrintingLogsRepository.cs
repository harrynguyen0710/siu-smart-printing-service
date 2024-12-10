using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.IRepositories
{
    public interface IPrintingLogsRepository : IRepository<PrintingLogs>
    {
        Task<IEnumerable<PrintingLogs>> GetAllPrintingLogsByUserId(string userId);
        Task<IEnumerable<PrintingLogs>> SearchPrintingLogsByName(string keyword);
        Task<IEnumerable<PrintingLogs>> GetAllPrintingLogsIByPrinterId(int printerId);
    }
}
