using System.Security.Claims;

namespace CookieBasedAuth.Service
{
    public interface IService
    {
        Task LogInAsync(string password, string username, bool DbRegister);
        Task LogOutAsync();
        Task SignInAsync(string password, string username);
        Task AddDbRoleAsync(string roleName);
        Task UserRoleAsync(string username, string rolename);
        Task AddDbUserClaimAsync(string username, IEnumerable<Claim> claims);
    }
}
