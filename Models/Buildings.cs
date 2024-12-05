using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace siu_smart_printing_service.Models
{
    public class Buildings
    {
        [Key]
        public int buildingId { get; set; }

        [Display(Name = "Buildings")]
        public string buildingName { get; set; } = string.Empty;

        [ForeignKey("Campus")]
        public int campusId { get; set; }
        public Campus campus { get; set; }  
        public ICollection<Rooms> rooms { get; set; }
    }
}
