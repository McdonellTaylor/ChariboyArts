using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CHARIBOY_ARTS.Data
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }
    }
}
