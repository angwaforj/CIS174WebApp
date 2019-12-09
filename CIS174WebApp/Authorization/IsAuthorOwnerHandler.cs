using System.Threading.Tasks;
using CIS174WebApp.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CIS174WebApp.Authorization
{
    public class IsAuthorOwnerHandler :AuthorizationHandler<IsAuthorOwnerRequirement, Author>
    {
        private readonly UserManager<AppUser> _userManager;

        public IsAuthorOwnerHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IsAuthorOwnerRequirement requirement, Author resource)
        {
            var appUser = await _userManager.GetUserAsync(context.User);
            if(appUser == null)
            {
                return;
            }

            if(resource.CreatedById == appUser.Id || appUser.State == resource.State)
            {
                context.Succeed(requirement);
            }
        }
    }
}
