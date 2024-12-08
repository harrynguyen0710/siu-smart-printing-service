using Microsoft.EntityFrameworkCore;
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

        public void ActivatePrinter(Printers printer)
        {
            printer.isActive = true;
        }
        public async Task<IEnumerable<Printers>> GetAllPrintersAsync() 
        {
            var printers = await _context.Printers
                                .Include(r => r.location)
                                .ToListAsync();
            return printers;
        }
        public async Task<IEnumerable<Printers>> GetAllActivePrinters()
        {
            var printers = await _context.Printers
                    .Where(p => p.isActive)
                    .Include(r => r.location)
                    .ToListAsync();
            return printers;
        }

        public async Task<IEnumerable<Printers>> GetAllDisabledPrinters()
        {
            var printers = await _context.Printers
                    .Where(p => !p.isActive)
                    .Include(r => r.location)
                    .ToListAsync();
            return printers;
        }

        public void InActivePrinter(Printers printer)
        {
            printer.isActive = false;
        }
    }
}
