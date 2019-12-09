using Microsoft.AspNetCore.Authorization;

namespace CIS174WebApp.Authorization
{
    public class MinimumAgeRequirement: IAuthorizationRequirement
    {
        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }

        public int MinimumAge { get; }
    }
}
