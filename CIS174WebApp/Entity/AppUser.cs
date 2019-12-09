using Microsoft.AspNetCore.Identity;

namespace CIS174WebApp.Entity
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }   

        public string City { get; set; }
        public string State { get; set; }
    }
}