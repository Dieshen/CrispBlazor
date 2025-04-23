namespace CrispBlazor.Modules
{
    public interface IModule
    {
        void MapModuleServices(IServiceCollection services);
        void MapModuleEndpoints(IEndpointRouteBuilder endpoints);
    }

    public static class ModuleExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services;
        }

        public static IEndpointRouteBuilder MapApplicationEndpoints(this IEndpointRouteBuilder endpoints)
        {
            return endpoints;
        }
    }
}
