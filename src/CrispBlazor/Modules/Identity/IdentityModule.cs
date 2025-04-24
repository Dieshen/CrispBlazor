
using CrispBlazor.Modules.Identity.Endpoints;

namespace CrispBlazor.Modules.Identity
{
    public class IdentityModule : IModule
    {
        public void RegisterModule(IServiceCollection services)
        {
        }

        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            RouteGroupBuilder group = endpoints.MapGroup(nameof(Identity));
            PerformExternalLoginEndpoint.Map(group);
            LogoutEndpoint.Map(group);

            RouteGroupBuilder manageGroup = group.MapGroup("/Manage").RequireAuthorization();
            LinkExternalLoginEndpoint.Map(manageGroup);
            DownloadPersonalDataEndpoint.Map(manageGroup);
        }
    }
}
