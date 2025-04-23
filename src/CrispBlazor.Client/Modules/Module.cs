using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Polly;
using Polly.Extensions.Http;

namespace CrispBlazor.Client.Modules
{
    public interface IModule
    {
        void MapModuleServices(WebAssemblyHostBuilder builder);
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    public static class ModuleExtensions
    {
        public static WebAssemblyHostBuilder AddApplicationServices(this WebAssemblyHostBuilder builder) => builder;
    }
}
