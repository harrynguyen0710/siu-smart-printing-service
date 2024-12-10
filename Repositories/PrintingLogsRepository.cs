using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<PrintingLogs>> GetAllPrintingLogs()
        {
            var query = await _context.PrintersLogs
                .Include(ft => ft.printer)
                .Include(pt => pt.uploadFile)
                .ToListAsync();
            return query;
        }

        public async Task<IEnumerable<PrintingLogs>> GetAllPrintingLogsByUserId(string userId)
        {
            var query = _context.PrintersLogs
                .Include(ft => ft.printer)
                .Include(pt => pt.uploadFile)
                                    .Where(u => u.uploadFile.userId == userId);


            return await query.ToListAsync();
        }

        public async Task<IEnumerable<PrintingLogs>> GetAllPrintingLogsIByPrinterId(int printerId)
        {
            var query = _context.PrintersLogs
                .Include(ft => ft.uploadFile)
                                    .Where(u => u.printerId == printerId);


            return await query.ToListAsync();
        }

        public async Task<IEnumerable<PrintingLogs>> SearchPrintingLogsByName(string keyword)
        {
            var printinglogs = await _context.PrintersLogs
                    .Where(t => t.uploadFile.fileName.Contains(keyword))
                    .ToListAsync();
            return printinglogs;

        }
    }
}
