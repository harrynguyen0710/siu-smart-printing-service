using Microsoft.EntityFrameworkCore;
using siu_smart_printing_service.Data;
using siu_smart_printing_service.IRepositories;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Repositories
{
    public class UploadedFileRepository : Repository<UploadFile>, IUploadedFileRepository
    {
        public UploadedFileRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<UploadFile>> GetAllFileByUserId(string userId)
        {
            var files = await _context.UploadFiles.Where(p => p.userId == userId).ToListAsync();
            return files;
        }


    }
}
