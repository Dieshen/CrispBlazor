using CrispBlazor.Client.Modules.ProjectManagement.Models;
using CrispBlazor.Client.Modules.ProjectManagement.Requests;
using CrispBlazor.Shared.Interfaces;
using CrispBlazor.Shared.Responses;

namespace CrispBlazor.Client.Modules.ProjectManagement.Services
{
    public interface IProjectService :
        ICreateService<CreateProject>,
        IQueryService<GetProject, Project>,
        IFilteredQueryService<GetProjects, Project>,
        IRequestService<UpdateProject>,
        IRequestService<DeleteProject>,
        IRequestService<ArchiveProject>
    {
    }

    internal sealed class ProjectService(HttpClient client) : IProjectService
    {
        private readonly HttpClient _client = client;

        public void Dispose() => throw new NotImplementedException();

        public ValueTask<Guid> Send(CreateProject request) => throw new NotImplementedException();

        public ValueTask<Project> Send(GetProject request) => throw new NotImplementedException();

        public ValueTask<FilteredResponse<Project>> Send(GetProjects request) => throw new NotImplementedException();

        public ValueTask Send(UpdateProject request) => throw new NotImplementedException();

        public ValueTask Send(DeleteProject request) => throw new NotImplementedException();

        public ValueTask Send(ArchiveProject request) => throw new NotImplementedException();
    }
}
