using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace siu_smart_printing_service.Models
{
    public class Printers
    {
        [Key]
        public int printerId {  get; set; }
        [Required(ErrorMessage = "Please enter printer name")]
        [Display(Name = "Name")]
        public string printerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter printer description")]
        [Display(Name = "Description")]
        public string description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter printer brand")]
        [Display(Name = "Brand")]
        public string brand { get; set; } = string.Empty;

        [Display(Name = "Paper count")]
        public int? paperCount { get; set; } = 0;

        [Display(Name = "Status")]
        public bool isActive { get; set; } = false;

        [ForeignKey("Romms")]
        public int roomId { get; set; }

        public Rooms location { get; set; }
        public ICollection<PrintingLogs> printingLogs { get; set; } 

    }
}
