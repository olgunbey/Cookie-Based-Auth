using System.ComponentModel.DataAnnotations;

namespace CookieBasedAuth.Entities
{
    public class Roles
    {
        [Key]
        public int ID { get; set; }
        public string? RoleName { get; set; }
        public ICollection<UserRoles>? UserRoles { get; set; }
        public ICollection<RoleClaims>? RoleClaims { get; set; }
    }
}
