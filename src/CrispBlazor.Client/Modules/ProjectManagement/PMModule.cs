using CrispBlazor.Client.Modules.ProjectManagement.Models;
using CrispBlazor.Client.Modules.ProjectManagement.Requests;
using CrispBlazor.Client.Modules.ProjectManagement.Services;
using CrispBlazor.Shared.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CrispBlazor.Client.Modules.ProjectManagement
{
    public class PMModule : IModule
    {
        public void RegisterModule(WebAssemblyHostBuilder builder)
        {
            void SetupClient(HttpClient client)
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            }

            builder.Services.AddHttpClient<ICreateService<CreateProject>, ProjectService>(SetupClient);
            builder.Services.AddHttpClient<IQueryService<GetProject, Project>, ProjectService>(SetupClient);
            builder.Services.AddHttpClient<IFilteredQueryService<GetProjects, Project>, ProjectService>(SetupClient);
            builder.Services.AddHttpClient<IRequestService<UpdateProject>, ProjectService>(SetupClient);
            builder.Services.AddHttpClient<IRequestService<DeleteProject>, ProjectService>(SetupClient);
            builder.Services.AddHttpClient<IRequestService<ArchiveProject>, ProjectService>(SetupClient);
        }
    }
}
