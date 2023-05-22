using CookieBasedAuth.Entities;
using CookieBasedAuth.Repository.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace CookieBasedAuth.Repository
{
    public class Repository : IRepository
    {
        private readonly IHttpContextAccessor httpContext;
        public Repository(IHttpContextAccessor http)
        {
            httpContext = http;

        }

        public async Task LogInAsync(string password,string username)
        {
            using (AppDbContext appDbContext = new())   
            {
            Users? users=await appDbContext.Users.FirstOrDefaultAsync(x=>x.userName == username && x.Password==password);
                if(users!=null) //burada doğru giriş yapıldı
                {
                    List<Claim> claims = new List<Claim>()
                    {
                    new Claim("Password",password),
                    new Claim("Username",username)
                    };
                    var claimIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                  await  httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimIdentity));
                }

            }
        }

        public async Task LogOutAsync()
        {
           await httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task SignInAsync(string password,string username) //kayit ol
        {
            using (AppDbContext appDbContext=new())
            {
              appDbContext.Users.Add(new Users() { userName = username,Password=password });
              await  appDbContext.SaveChangesAsync();
            }
        }
    }
}
