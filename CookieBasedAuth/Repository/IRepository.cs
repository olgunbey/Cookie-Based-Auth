namespace CookieBasedAuth.Repository
{
    public interface IRepository
    {
        Task LogInAsync(string password, string username);
        Task LogOutAsync();
        Task SignInAsync(string password, string username);
    }
}
