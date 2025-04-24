namespace CrispBlazor.Modules
{
    public interface IModule
    {
        void RegisterModule(IServiceCollection services);
        void MapEndpoints(IEndpointRouteBuilder endpoints);
    }

    public static class ModuleExtensions
    {
        private static readonly List<IModule> registeredModules = [];
        public static IServiceCollection RegisterModules(this IServiceCollection services)
        {
            IEnumerable<IModule> modules = DiscoverModules();
            foreach (IModule module in modules)
            {
                module.RegisterModule(services);
                registeredModules.Add(module);
            }

            return services;
        }

        public static WebApplication MapEndpoints(this WebApplication app)
        {
            foreach (IModule module in registeredModules)
            {
                module.MapEndpoints(app);
            }
            return app;
        }

        private static IEnumerable<IModule> DiscoverModules() =>
            typeof(IModule).Assembly
                           .GetTypes()
                           .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
                           .Select(Activator.CreateInstance)
                           .Cast<IModule>();
    }
}
