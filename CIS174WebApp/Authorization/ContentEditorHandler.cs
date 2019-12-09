using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CIS174WebApp.Authorization
{
    public class ContentEditorHandler: AuthorizationHandler<ContentEditorRequirement>
    {
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ContentEditorRequirement requirement)
        {
            if(context.User.HasClaim(c => c.Type == Claims.ContentEditor))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
