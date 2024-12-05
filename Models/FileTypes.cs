using System.ComponentModel.DataAnnotations;

namespace siu_smart_printing_service.Models
{
    public class FileTypes
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Type")]
        public string name { get; set; }
        [Display(Name = "Description")]
        public string description { get; set; } = string.Empty;
        [Display(Name = "Status")]
        public bool isAccepted { get; set; }
        public ICollection<UploadFile> files { get; set; }
    }
}
