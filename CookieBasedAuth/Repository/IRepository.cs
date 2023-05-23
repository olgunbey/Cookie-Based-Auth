using CookieBasedAuth.Entities;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace CookieBasedAuth.Repository
{
    public interface IRepository
    {
        Task LogInAsync(string password, string username,bool DbRegister);
        Task LogOutAsync();
        Task SignInAsync(string password, string username);
        Task AddDbRoleAsync(string roleName);
        Task UserRoleAsync(string username, string rolename);
        Task AddDbUserClaimAsync(string username,IEnumerable<Claim> claims);
    }
}
