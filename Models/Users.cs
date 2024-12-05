using Microsoft.AspNetCore.Identity;

namespace siu_smart_printing_service.Models
{
    public class Users : IdentityUser
    {
        public string fullName { get; set; } = string.Empty;
        public string major { get; set; } = string.Empty;
        public double balance { get; set; }
        public ICollection<UploadFile> UploadFiles { get; set; }
    }
}
