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

        public async Task<IEnumerable<PrintingLogs>> GetAllPrintingLogsByUserId(string userId, int page, int pageSize, bool ascOrder = true)
        {
            var query = _context.PrintersLogs
                                    .Where(u => u.uploadFile.userId == userId);

            query = ascOrder ? query.OrderBy(u => u.startDate) : query.OrderByDescending(u => u.startDate);

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<PrintingLogs>> GetAllPrintingLogsIByPrinterId(int printerId, int page, int pageSize, bool ascOrder = true)
        {
            var query = _context.PrintersLogs
                                    .Where(u => u.printerId == printerId);

            query = ascOrder ? query.OrderBy(u => u.startDate) : query.OrderByDescending(u => u.startDate);

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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
