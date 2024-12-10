using System.ComponentModel.DataAnnotations;

namespace siu_smart_printing_service.Models
{
    public class Configurations
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Default Page Balance")]
        public int defaultBalance { get; set; }
        [Display(Name = "Max File Size")]
        public long maxSizeBytes { get; set; }
        [Display(Name = "Granted Date")]
        public DateTime defaultDate { get; set; }
    }
}
