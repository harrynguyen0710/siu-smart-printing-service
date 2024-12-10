using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.ViewModels
{
    public class ConfigurationViewModel
    {
        public IEnumerable<Configurations> Configs {  get; set; }
        public IEnumerable<FileTypes> FileTypes { get; set; }   
        public Configurations Config { get; set; }
    }
}
