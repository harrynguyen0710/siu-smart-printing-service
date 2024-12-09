using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.IRepositories
{
    public interface IFileTypesRepository : IRepository<FileTypes>
    {
        Task<IEnumerable<FileTypes>> GetAllDisableFileTypes();
        Task<IEnumerable<FileTypes>> GetAllActiveFileTypes();

        void InActiveFile(FileTypes fileTypes);
        void ActiveFile(FileTypes fileTypes);

        Task<FileTypes> GetFileTypeByName(string fileType);

    }
}
