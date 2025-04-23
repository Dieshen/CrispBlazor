using CrispBlazor.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CrispBlazor.Modules.Identity.Endpoints
{
    public class DownloadPersonalDataEndpoint : IEndpoint
    {
        public static void Map(RouteGroupBuilder builder)
        {
            builder.MapPost("/DownloadPersonalData", async (
                HttpContext context,
                [FromServices] UserManager<ApplicationUser> userManager,
                [FromServices] AuthenticationStateProvider authenticationStateProvider,
                [FromServices] ILoggerFactory loggerFactory) =>
            {
                ILogger downloadLogger = loggerFactory.CreateLogger("DownloadPersonalData");

                ApplicationUser? user = await userManager.GetUserAsync(context.User);
                if (user is null)
                {
                    return Results.NotFound($"Unable to load user with ID '{userManager.GetUserId(context.User)}'.");
                }

                string userId = await userManager.GetUserIdAsync(user);
                downloadLogger.LogInformation("User with ID '{UserId}' asked for their personal data.", userId);

                // Only include personal data for download
                var personalData = new Dictionary<string, string>();
                IEnumerable<System.Reflection.PropertyInfo> personalDataProps = typeof(ApplicationUser).GetProperties().Where(
                    prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
                foreach (System.Reflection.PropertyInfo? p in personalDataProps)
                {
                    personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
                }

                IList<UserLoginInfo> logins = await userManager.GetLoginsAsync(user);
                foreach (UserLoginInfo l in logins)
                {
                    personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
                }

                personalData.Add("Authenticator Key", (await userManager.GetAuthenticatorKeyAsync(user))!);
                byte[] fileBytes = JsonSerializer.SerializeToUtf8Bytes(personalData);

                context.Response.Headers.TryAdd("Content-Disposition", "attachment; filename=PersonalData.json");
                return TypedResults.File(fileBytes, contentType: "application/json", fileDownloadName: "PersonalData.json");
            });
        }
    }
}
