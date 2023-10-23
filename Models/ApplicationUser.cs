using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace FoodApplication.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name {  get; set; }
        public string Address {  get; set; }

    }
}
