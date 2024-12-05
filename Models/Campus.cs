using System.ComponentModel.DataAnnotations;

namespace siu_smart_printing_service.Models
{
    public class Campus
    {
        [Key]
        public int campusId {  get; set; }
        [Display(Name = "Campus Name")]
        public string campusName { get; set; } = string.Empty;

        [Display(Name = "Address")]
        public string address { get; set; } = string.Empty;
        public ICollection<Buildings> buildings { get; set; }
    }
}
