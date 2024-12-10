using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.IRepositories
{
    public interface IUploadedFileRepository : IRepository<UploadFile>
    {
        Task<IEnumerable<UploadFile>> GetAllFileByUserId(string userId);
        Task<UploadFile> GetFileById(int fileId);

    }
}
