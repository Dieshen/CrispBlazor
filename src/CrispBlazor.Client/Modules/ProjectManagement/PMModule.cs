using CrispBlazor.Client.Modules.ProjectManagement.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CrispBlazor.Client.Modules.ProjectManagement
{
    public class PMModule : IModule
    {
        public void MapModuleServices(WebAssemblyHostBuilder builder) =>
            builder.Services.AddHttpClient<IProjectService, ProjectService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
    }
}
