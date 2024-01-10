using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace uyg05_IdentityApp.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
