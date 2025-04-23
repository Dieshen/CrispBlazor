using CrispBlazor.Data;
using CrispBlazor.Modules.Identity.Pages.Manage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CrispBlazor.Modules.Identity.Endpoints
{
    public sealed class LinkExternalLoginEndpoint : IEndpoint
    {
        public static void Map(RouteGroupBuilder builder)
        {
            builder.MapPost("/LinkExternalLogin", async (
                HttpContext context,
                [FromServices] SignInManager<ApplicationUser> signInManager,
                [FromForm] string provider) =>
            {
                // Clear the existing external cookie to ensure a clean login process
                await context.SignOutAsync(IdentityConstants.ExternalScheme);

                string redirectUrl = UriHelper.BuildRelative(
                    context.Request.PathBase,
                    "/Account/Manage/ExternalLogins",
                    QueryString.Create("Action", ExternalLogins.LinkLoginCallbackAction));

                AuthenticationProperties properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, signInManager.UserManager.GetUserId(context.User));
                return TypedResults.Challenge(properties, [provider]);
            });
        }
    }
}
