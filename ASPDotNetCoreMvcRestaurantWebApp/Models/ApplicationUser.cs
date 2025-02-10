using Microsoft.AspNetCore.Identity;

namespace ASPDotNetCoreMvcRestaurantWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }
    }
}
