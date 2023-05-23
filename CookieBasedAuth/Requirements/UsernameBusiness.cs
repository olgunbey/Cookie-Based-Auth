using CookieBasedAuth.Entities;
using CookieBasedAuth.Repository.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CookieBasedAuth.Requirements
{
    public class UsernameBusiness:IAuthorizationRequirement
    {

    }
    public class UsernameHandler : AuthorizationHandler<UsernameBusiness>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UsernameBusiness requirement)
        {
            
         Claim claims= context.User.Claims.FirstOrDefault(x => x.Type=="Username"); //cookie içinden çeker
            using (AppDbContext dbContext=new())
            {
                if (claims==null)
                {
                    var userClaims = (await dbContext.UserClaims.Where(x => x.ClaimType == "Username").ToListAsync()).Select(x => x.ClaimValue).ToList();
                    if (!userClaims.Contains("ahmetbey"))
                    {
                        context.Fail();
                        return;
                    }
                    return;
                }
            }
            context.Succeed(requirement);
            return;
        }
    }
}
