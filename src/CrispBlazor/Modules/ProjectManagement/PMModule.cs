using CrispBlazor.Client.Modules.ProjectManagement.Models;
using CrispBlazor.Client.Modules.ProjectManagement.Requests;
using CrispBlazor.Modules.ProjectManagement.Endpoints;
using CrispBlazor.Shared.Interfaces;
using CrispBlazor.Shared.Utilities;
using Humanizer;

namespace CrispBlazor.Modules.ProjectManagement
{
    public class PMModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            string route = new ApiRouteBuilder().WithGroupName(nameof(Project).Pluralize()).Build();
            RouteGroupBuilder group = endpoints.MapGroup(route)
                .AddEndpointFilter<ExceptionFilter>()
                .WithTags(nameof(Project).Pluralize());

            CreateProjectEndpoint.Map(group);
            GetProjectEndpoint.Map(group);
            GetProjectsEndpoint.Map(group);
        }

        public void RegisterModule(IServiceCollection services)
        {
            services.AddScoped<ICreateService<CreateProject>, CreateProjectService>();
            services.AddScoped<IRequestService<GetProject, Project>, GetProjectService>();
            services.AddScoped<IFilteredQueryService<GetProjects, Project>, GetProjectsService>();
        }
    }
}
