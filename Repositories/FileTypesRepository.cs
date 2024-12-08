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
    }
}
