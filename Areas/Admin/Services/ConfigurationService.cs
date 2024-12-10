using siu_smart_printing_service.IRepositories;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Areas.Admin.Services
{
    public class ConfigurationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ConfigurationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Configurations>> GetAll()
        {
            var configs = await _unitOfWork.ConfigurationRepository.GetAllAsync();  
            return configs;
        }

        public async Task<Configurations> GetById(int id)
        {
            var config = await _unitOfWork.ConfigurationRepository.GetByIdAsync(id);
            return config;
        }

        public async Task Update(Configurations configurations) 
        {
            _unitOfWork.ConfigurationRepository.Update(configurations);
            await _unitOfWork.CompleteAsync();
        }

    }
}
