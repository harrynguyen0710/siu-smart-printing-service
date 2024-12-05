using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace siu_smart_printing_service.Models
{
    public class PrintingLogs
    {
        [Key]
        public int id {  get; set; }

        [Display(Name = "Start Date")]
        public DateTime? startDate { get; set; } = DateTime.Now;

        [Display(Name = "End Date")]
        public DateTime? endDate { get; set; }

        [Display(Name = "Page Size")]
        [Required(ErrorMessage = "Please choose a page size")]
        public string pageSize { get; set; } = String.Empty;

        [Display(Name = "Printing Side")]
        [Required(ErrorMessage = "Please choose a printing side")]
        public bool isDoubleSided { get; set; }

        [Display(Name = "Number of Copies")]
        [Required(ErrorMessage = "Please enter the number of copies")]
        public int numberOfCopies { get; set; }

        [Display(Name = "Status")]
        public string? status { get; set; } = "In Progress";

        [ForeignKey("UploadFile")]
        public int uploadFileId { get; set; }   
        public UploadFile uploadFile { get; set; }


        [ForeignKey("Printers")]
        public int printerId { get; set; }
        public Printers printer { get; set; }  
    }
}
