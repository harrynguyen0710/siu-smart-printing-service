using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace siu_smart_printing_service.Models
{
    public class UploadFile
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "File name")]
        public string fileName { get; set; }

        [Display(Name = "Upload time")]
        public DateTime updatedDate { get; set; }

        [Display(Name = "File size")]
        public string fileSize { get;set; }
        public int fileTypeId { get; set; }
        public FileTypes fileTypes { get; set; }

        public ICollection<PrintingLogs> printerLogs { get; set; }
        
        [ForeignKey("Users")]
        public string userId { get; set; }
        public Users student { get; set; }  

    }
}
