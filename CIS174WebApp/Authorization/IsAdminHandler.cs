using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CIS174WebApp.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CIS174WebApp.Authorization
{
    public class IsAdminHandler: AuthorizationHandler<IsAdminHandler>, IAuthorizationRequirement
    {
        private readonly SignInManager<AppUser> _signInManager;

        public IsAdminHandler(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminHandler requirement)
        {
            if(context.User.HasClaim(c => c.Type == Claims.IsAdmin))
            {
                context.Succeed(requirement);
            }
            

            return Task.CompletedTask;
        }
    }
}
