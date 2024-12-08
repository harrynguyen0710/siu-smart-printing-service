using siu_smart_printing_service.Data;
using siu_smart_printing_service.IRepositories;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Repositories
{
    public class PrinterRepository : Repository<Printers>, IPrinterRepository
    {
        public PrinterRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
