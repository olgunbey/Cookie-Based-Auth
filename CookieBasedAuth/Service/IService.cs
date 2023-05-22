namespace CookieBasedAuth.Service
{
    public interface IService
    {
        Task LogInAsync(string password, string username);
        Task LogOutAsync();
        Task SignInAsync(string password, string username);
    }
}
