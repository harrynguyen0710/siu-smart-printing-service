using Microsoft.VisualBasic.FileIO;
using siu_smart_printing_service.IRepositories;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Areas.Admin.Services
{
    public class FileTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FileTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(FileTypes fileType)
        {
            _unitOfWork.FileTypesRepository.Add(fileType);
            await _unitOfWork.CompleteAsync();
        }

        public async Task InActivate(FileTypes fileType)
        {
            _unitOfWork.FileTypesRepository.InActiveFile(fileType);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Activate(FileTypes fileType)
        {
            _unitOfWork.FileTypesRepository.ActiveFile(fileType);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<FileTypes>> GetAllAsync()
        {
            var fileTypes = await _unitOfWork.FileTypesRepository.GetAllAsync();
            return fileTypes;
        }

        public async Task<IEnumerable<FileTypes>> GetAllAcceptedFileTypes() 
        {
            var fileTypes = await _unitOfWork.FileTypesRepository.GetAllActiveFileTypes();
            return fileTypes;
        }

        public async Task<IEnumerable<FileTypes>> GetAllDisabledFileTypes()
        {
            var fileTypes = await _unitOfWork.FileTypesRepository.GetAllDisableFileTypes();
            return fileTypes;
        }
    }
}
