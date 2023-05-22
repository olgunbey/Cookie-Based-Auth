using System.ComponentModel.DataAnnotations;

namespace CookieBasedAuth.Entities
{
    public class Users
    {
        [Key]
        public int ID { get; set; }
        public string? userName{ get; set; }
        public string? Password { get; set; }
        public ICollection<UserRoles>? UserRoles { get; set; }
        public ICollection<UserClaims>? UserClaims { get; set; }
    }
}
