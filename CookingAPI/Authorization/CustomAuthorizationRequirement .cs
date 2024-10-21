using Microsoft.AspNetCore.Authorization;

namespace CookingAPI.Authorization
{
    public class CustomAuthorizationRequirement : IAuthorizationRequirement
    {
        public string RequiredClaim { get; }

        public CustomAuthorizationRequirement(string requiredClaim)
        {
            RequiredClaim = requiredClaim;
        }
    }
}
