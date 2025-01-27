using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeInfo.Models
{
    public class AppUser: IdentityUser
    {
        public string? Name { get; set; }
        [NotMapped]

        public IList <string>? RoleNames { get; set; }

        public DateTime SignUpdate { get; set; }
    }
}

