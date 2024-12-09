using siu_smart_printing_service.IRepositories;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Areas.Admin.Services
{
    public class RoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Rooms>> GetAllAsync()
        {
            var rooms = await _unitOfWork.RoomRepository.GetAllAsync();
            return rooms;
        }

    }
}
