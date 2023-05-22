using CookieBasedAuth.Repository;

namespace CookieBasedAuth.Service
{
    public class Service : IService
    {
        private readonly IRepository _repository;
        public Service(IRepository repository)
        {
            _repository = repository;
        }
        public async Task LogInAsync(string password, string username)
        {
           await _repository.LogInAsync(password, username);
        }

        public async Task LogOutAsync()
        {
           await _repository.LogOutAsync();
        }

        public async Task SignInAsync(string password, string username)
        {
         await   _repository.SignInAsync(password, username);
        }
    }
}
