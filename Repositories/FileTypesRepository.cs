using Microsoft.EntityFrameworkCore;
using siu_smart_printing_service.Data;
using siu_smart_printing_service.IRepositories;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Repositories
{
    public class FileTypesRepository : Repository<FileTypes>, IFileTypesRepository
    {
        public FileTypesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void InActiveFile(FileTypes fileTypes)
        {
            fileTypes.isAccepted = false;
        }
        public void ActiveFile(FileTypes fileTypes)
        {
            fileTypes.isAccepted = true;
        }

        public async Task<FileTypes> GetFileTypeByName(string fileType)
        {
            var type = await _context.FileTypes.Where(p => p.name == fileType).FirstOrDefaultAsync();
            return type;
        }


        public async Task<IEnumerable<FileTypes>> GetAllDisableFileTypes()
        {
            var fileTypes = await _context.FileTypes.Where(p => !p.isAccepted).ToListAsync();
            return fileTypes;
        }

        public async Task<IEnumerable<FileTypes>> GetAllActiveFileTypes()
        {
            var fileTypes = await _context.FileTypes.Where(p => p.isAccepted).ToListAsync();
            return fileTypes;
        }
    }
}
