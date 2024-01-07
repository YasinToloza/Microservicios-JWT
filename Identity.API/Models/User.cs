using Microsoft.AspNetCore.Identity;

namespace Identity.API.Models
{
    public class User:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
