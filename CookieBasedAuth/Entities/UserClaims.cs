using System.ComponentModel.DataAnnotations;

namespace CookieBasedAuth.Entities
{
    public class UserClaims
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
        public Users? User { get; set; }
    }
}
