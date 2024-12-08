using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.IRepositories
{
    public interface IPrinterRepository : IRepository<Printers>
    {
        void InActivePrinter(Printers printer);
        void ActivatePrinter(Printers printer);
        Task<IEnumerable<Printers>> GetAllAsync();
        Task<IEnumerable<Printers>> GetAllDisabledPrinters();
        Task<IEnumerable<Printers>> GetAllActivePrinters();

    }
}
