using siu_smart_printing_service.IRepositories;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Services
{
    public class PrintingLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PrintingLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PrintingLogs>> GetAllAsync()
        {
            var logs = await _unitOfWork.PrintingLogsRepository.GetAllPrintingLogs();
            return logs;
        }

        public async Task PrintDocument(PrintingLogs printingLog)
        {
            _unitOfWork.PrintingLogsRepository.Add(printingLog);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<PrintingLogs>> GetAllPrintingLogsByUserId(string userId)
        {
            var printingLogs = await _unitOfWork.PrintingLogsRepository.GetAllPrintingLogsByUserId(userId);
            return printingLogs;
        }

        public async Task<IEnumerable<PrintingLogs>> GetAllPrintingLogsIByPrinterId(int printerId)
        {
            var printingLogs = await _unitOfWork.PrintingLogsRepository.GetAllPrintingLogsIByPrinterId(printerId);
            return printingLogs;
        }

        public async Task<IEnumerable<PrintingLogs>> SearchPrintingLogsByName(string keyword)
        {
            var priningLogs = await _unitOfWork.PrintingLogsRepository.SearchPrintingLogsByName(keyword);
            return priningLogs;
        }
    }
}
