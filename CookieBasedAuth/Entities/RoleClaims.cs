using System.ComponentModel.DataAnnotations;

namespace CookieBasedAuth.Entities
{
    public class RoleClaims
    {
        [Key]
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
        public Roles? Role { get; set; }
    }
}
