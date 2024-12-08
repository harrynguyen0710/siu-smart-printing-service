using siu_smart_printing_service.Data;
using siu_smart_printing_service.IRepositories;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Repositories
{
    public class PrintingLogsRepository : Repository<PrintingLogs>, IPrintingLogsRepository
    {
        public PrintingLogsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
