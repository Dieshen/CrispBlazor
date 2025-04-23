using CrispBlazor.Client.Modules.ProjectManagement.Models;
using CrispBlazor.Shared.Requests;

namespace CrispBlazor.Client.Modules.ProjectManagement.Requests
{
    public sealed record GetProject : SingularQuery<Project>;
    public sealed record GetProjects : PagedQuery<Project>;
}
