using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace siu_smart_printing_service.Models
{
    public class Rooms
    {
        [Key]
        public int roomId { get; set; }
        [Display(Name = "Location")]
        public string roomName { get; set; } = string.Empty;

        [ForeignKey("Buildings")]
        public int buildingId { get; set; } 
        public Buildings building { get; set; }   
        public ICollection<Printers> printers { get; set; }
    }
}
