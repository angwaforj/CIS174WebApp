using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CIS174WebApp.Authorization
{
    public class DateOfBirthHandler :AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var dateOfBirthClaim = context.User.FindFirstValue(ClaimTypes.DateOfBirth);

            if (dateOfBirthClaim == null)
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = Convert.ToDateTime(dateOfBirthClaim);
            var cutoff = dateOfBirth.AddYears(requirement.MinimumAge);

            if(cutoff < DateTime.Today)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
