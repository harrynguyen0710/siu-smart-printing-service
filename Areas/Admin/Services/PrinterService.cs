using siu_smart_printing_service.IRepositories;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Areas.Admin.Services
{
    public class PrinterService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PrinterService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddPrinter(Printers printer)
        {
            _unitOfWork.PrinterRepository.Add(printer); 
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Printers>> GetAllPrinters()
        {
            var printers = await _unitOfWork.PrinterRepository.GetAllPrintersAsync();
            return printers;
        }

        public async Task<IEnumerable<Printers>> GetAllActivePrinters()
        {
            var printers = await _unitOfWork.PrinterRepository.GetAllActivePrinters();
            return printers;
        }

        public async Task<IEnumerable<Printers>> GetAllDisabledPrinters()
        {
            var printers = await _unitOfWork.PrinterRepository.GetAllDisabledPrinters();
            return printers;
        }

        public async Task InActivePrinter(Printers printer) 
        {
            _unitOfWork.PrinterRepository.InActivePrinter(printer); 
            await _unitOfWork.CompleteAsync();
        }

        public async Task ActivatePrinter(Printers printer)
        {
            _unitOfWork.PrinterRepository.ActivatePrinter(printer);
            await _unitOfWork.CompleteAsync();
        }

    }
}
