using siu_smart_printing_service.IRepositories;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Services
{
    public class UploadedFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UploadedFileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UploadFile>> GetAllFileByUserId(string userId)
        {
            var files = await _unitOfWork.UploadedFileRepository.GetAllFileByUserId(userId);
            return files;
        }

        public async Task Add(UploadFile file)
        {
            _unitOfWork.UploadedFileRepository.Add(file);
            await _unitOfWork.CompleteAsync();
        }

    }
}
