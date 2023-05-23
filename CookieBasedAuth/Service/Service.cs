using CookieBasedAuth.Repository;
using System.Security.Claims;

namespace CookieBasedAuth.Service
{
    public class Service : IService
    {
        private readonly IRepository _repository;
        public Service(IRepository repository)
        {
            _repository = repository;
        }

        public async Task AddDbRoleAsync(string roleName)
        {
           await _repository.AddDbRoleAsync(roleName);
        }

        public async Task AddDbUserClaimAsync(string username, IEnumerable<Claim> claims)
        {
          await _repository.AddDbUserClaimAsync(username, claims);
        }

        public async Task LogInAsync(string password, string username, bool DbRegister)
        {
           await _repository.LogInAsync(password, username,DbRegister);
        }

        public async Task LogOutAsync()
        {
           await _repository.LogOutAsync();
        }

        public async Task SignInAsync(string password, string username)
        {
         await   _repository.SignInAsync(password, username);
        }

        public async Task UserRoleAsync(string username, string rolename)
        {
            await _repository.UserRoleAsync(username, rolename);
        }
    }
}
