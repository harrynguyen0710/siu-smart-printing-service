using siu_smart_printing_service.IRepositories;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Update(Users user)
        {
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }

    }
}
