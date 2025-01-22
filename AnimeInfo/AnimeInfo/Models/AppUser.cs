using Microsoft.AspNetCore.Identity;

namespace AnimeInfo.Models
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public DateTime SignUpdate { get; set; }
    }
}