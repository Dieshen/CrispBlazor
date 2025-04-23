using CrispBlazor.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CrispBlazor.Modules.Identity.Endpoints
{
    public sealed class LogoutEndpoint : IEndpoint
    {
        public static void Map(RouteGroupBuilder builder)
        {
            builder.MapPost("/Logout", async (
                ClaimsPrincipal user,
                [FromServices] SignInManager<ApplicationUser> signInManager,
                [FromForm] string returnUrl) =>
            {
                await signInManager.SignOutAsync();
                return TypedResults.LocalRedirect($"~/{returnUrl}");
            });
        }
    }
}
