using CookieBasedAuth.Entities;
using CookieBasedAuth.Repository.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
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
        public async Task UserRoleAsync(string username,string rolename)
        {
            using(AppDbContext dbContext=new())
            {
              Users users=await dbContext.Users.FirstOrDefaultAsync(x=>x.userName==username);
              Roles roles=await dbContext.Roles.FirstOrDefaultAsync(x=>x.RoleName==rolename);
              dbContext.UserRoles.Add(new UserRoles() { RoleID = roles.ID, UserID = users.ID });
              await dbContext.SaveChangesAsync();

            }
            
        }
        public async Task AddDbRoleAsync(string roleName)
        {
            using (AppDbContext appDbContext=new())
            {
              Roles addedRole= await appDbContext.Roles.FirstOrDefaultAsync(x=>x.RoleName==roleName);
                if(addedRole==null)
                {
                    appDbContext.Roles.Add(new Roles() { RoleName = roleName });
                    await  appDbContext.SaveChangesAsync();
                }
            }

        }

        public async Task LogInAsync(string password,string username, bool DbRegister)
        {
            using (AppDbContext appDbContext = new())   
            {
                Users? users = await appDbContext.Users.FirstOrDefaultAsync(x => x.userName == username && x.Password == password);
                IEnumerable<Claim> claims = ClaimsDerive(users!);
                if (!DbRegister) //burada cookie içine claimler aktarılır
                {
                    if (users != null) //burada doğru giriş yapıldı
                    {
                        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var property = new AuthenticationProperties() { RedirectUri = "Home/SignIn" };
                        await httpContext.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity),property);
                    }
                }
                else //burada cookie'ye yazma db'ye yaz istenir.
                {
                    var Identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    await httpContext.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(Identity));
                    await AddDbUserClaimAsync(username,claims);
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

        public async Task AddDbUserClaimAsync(string username,IEnumerable<Claim> claims)
        {
            using (AppDbContext appDbContext=new())
            {
             Users? users= await appDbContext.Users.FirstOrDefaultAsync(x => x.userName == username);
                if(!appDbContext.UserClaims.Any(x=>x.ClaimValue==username))
                {
                    claims.ToList().ForEach(async x =>
                    {
                        await appDbContext.UserClaims.AddAsync(new UserClaims()
                        {
                            UserID = users.ID,
                            ClaimType = x.Type,
                            ClaimValue = x.Value,
                        });
                    });
                }
               await appDbContext.SaveChangesAsync();
                
            }
        }

        public IEnumerable<Claim> ClaimsDerive(Users users)
        {
                yield return new Claim("Username", users.userName!);
                yield return new Claim("Password", users.Password!);
        }
    }
}
