using Microsoft.AspNetCore.Identity;

namespace Blogging_Platform.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
       
        public string Name { get; set; }

        public string? ProfilePicture { get; set; }

    }
}
