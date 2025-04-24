using CrispBlazor.Client.Modules.ProjectManagement.Models;
using CrispBlazor.Client.Modules.ProjectManagement.Requests;
using CrispBlazor.Shared;
using CrispBlazor.Shared.Interfaces;
using CrispBlazor.Shared.Responses;
using CrispBlazor.Shared.Utilities;
using System.Net.Http.Json;

namespace CrispBlazor.Client.Modules.ProjectManagement.Services
{
    internal sealed class ProjectService(HttpClient client) :
        ICreateService<CreateProject>,
        IQueryService<GetProject, Project>,
        IFilteredQueryService<GetProjects, Project>,
        IRequestService<UpdateProject>,
        IRequestService<DeleteProject>,
        IRequestService<ArchiveProject>
    {
        private readonly HttpClient _client = client;

        public void Dispose() { }

        public async ValueTask<Guid> Send(CreateProject request)
        {
            string route = new ApiRouteBuilder().WithGroupName(nameof(Project)).Build();
            JsonContent content = JsonContent.Create(request);
            HttpResponseMessage httpResponse = await _client.PostAsync(route, content);
            return httpResponse.IsSuccessStatusCode
                ? await httpResponse.Content.ReadFromJsonAsync<Guid>()
                : throw new DomainException($"{httpResponse.StatusCode}, {await httpResponse.Content.ReadAsStringAsync()}");
        }

        public async ValueTask<Project> Send(GetProject request)
        {
            string route = new ApiRouteBuilder().WithGroupName(nameof(Project)).WithId(request.Id).Build();
            return await _client.GetFromJsonAsync<Project>(route) ?? new();
        }

        public async ValueTask<FilteredResponse<Project>> Send(GetProjects request)
        {
            string route = new ApiRouteBuilder().WithGroupName(nameof(Project)).Build();
            return await _client.GetFromJsonAsync<FilteredResponse<Project>>(route) ?? new();
        }

        public async ValueTask Send(UpdateProject request)
        {
            string route = new ApiRouteBuilder().WithGroupName(nameof(Project)).WithId(request.Id).Build();
            JsonContent content = JsonContent.Create(request);
            HttpResponseMessage httpResponse = await _client.PutAsync(route, content);
            if (!httpResponse.IsSuccessStatusCode)
                throw new DomainException($"{httpResponse.StatusCode}, {await httpResponse.Content.ReadAsStringAsync()}");
        }

        public async ValueTask Send(DeleteProject request)
        {
            string route = new ApiRouteBuilder().WithGroupName(nameof(Project)).WithId(request.Id).Build();
            HttpResponseMessage httpResponse = await _client.DeleteAsync(route);
            if (!httpResponse.IsSuccessStatusCode)
                throw new DomainException($"{httpResponse.StatusCode}, {await httpResponse.Content.ReadAsStringAsync()}");
        }

        public async ValueTask Send(ArchiveProject request)
        {
            string route = new ApiRouteBuilder().WithGroupName(nameof(Project)).WithId(request.Id).WithVerb(ApiVerb.Archive).Build();
            HttpResponseMessage httpResponse = await _client.PutAsync(route, null);
            if (!httpResponse.IsSuccessStatusCode)
                throw new DomainException($"{httpResponse.StatusCode}, {await httpResponse.Content.ReadAsStringAsync()}");
        }
    }
}
