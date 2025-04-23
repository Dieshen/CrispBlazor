using CrispBlazor.Data;
using CrispBlazor.Modules.Identity.Pages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CrispBlazor.Modules.Identity.Endpoints
{
    public sealed class PerformExternalLoginEndpoint : IEndpoint
    {
        public static void Map(RouteGroupBuilder builder)
        {
            builder.MapPost("/PerformExternalLogin", (
                HttpContext context,
                [FromServices] SignInManager<ApplicationUser> signInManager,
                [FromForm] string provider,
                [FromForm] string returnUrl) =>
            {
                IEnumerable<KeyValuePair<string, StringValues>> query = [
                    new("ReturnUrl", returnUrl),
                    new("Action", ExternalLogin.LoginCallbackAction)];

                string redirectUrl = UriHelper.BuildRelative(
                    context.Request.PathBase,
                    "/Account/ExternalLogin",
                    QueryString.Create(query));

                AuthenticationProperties properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
                return TypedResults.Challenge(properties, [provider]);
            });
        }
    }
}
