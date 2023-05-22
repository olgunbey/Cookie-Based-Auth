using System.ComponentModel.DataAnnotations;

namespace CookieBasedAuth.Entities
{
    public class UserRoles
    {
        [Key]
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public Users? Users { get; set; }
        public Roles? Roles { get; set; }

    }
}
