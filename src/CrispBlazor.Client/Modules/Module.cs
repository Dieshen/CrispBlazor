using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Polly;
using Polly.Extensions.Http;

namespace CrispBlazor.Client.Modules
{
    public interface IModule
    {
        void RegisterModule(WebAssemblyHostBuilder builder);
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    public static class ModuleExtensions
    {
        private static readonly List<IModule> registeredModules = [];
        public static WebAssemblyHostBuilder RegisterModules(this WebAssemblyHostBuilder builder)
        {
            IEnumerable<IModule> modules = DiscoverModules();
            foreach (IModule module in modules)
            {
                module.RegisterModule(builder);
                registeredModules.Add(module);
            }

            return builder;
        }

        private static IEnumerable<IModule> DiscoverModules() =>
            typeof(IModule).Assembly
                           .GetTypes()
                           .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
                           .Select(Activator.CreateInstance)
                           .Cast<IModule>();
    }
}
