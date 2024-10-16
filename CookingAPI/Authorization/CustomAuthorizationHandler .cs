using Microsoft.AspNetCore.Authorization;


namespace CookingAPI.Authorization
{

    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
        {
            // Verifica si el usuario tiene el claim requerido
            if (context.User.HasClaim(c => c.Type == requirement.RequiredClaim))
            {
                context.Succeed(requirement); // Si tiene el claim, la autorización es exitosa
            }

            return Task.CompletedTask;
        }
    }

}
