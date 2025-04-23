
using CrispBlazor.Client.Modules.ProjectManagement.Models;
using CrispBlazor.Client.Modules.ProjectManagement.Requests;
using CrispBlazor.Shared.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrispBlazor.Modules.ProjectManagement.Endpoints
{
    public class GetProjectEndpoint : BaseEndpoint, IGetByIdEndpoint<GetProject, Project>
    {

        public static void Map(RouteGroupBuilder builder) => builder.MapGet("{id:guid}", Handle)
                .Produces<Project>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound);

        public static async Task<IResult> Handle([FromRoute] Guid id, [FromServices] IRequestService<GetProject, Project> modelService, [FromServices] IEnumerable<IValidator<GetProject>> validators)
        {
            GetProject query = new() { Id = id };
            await ValidateRequest(query, validators);
            Project? project = await modelService.Send(query);
            return project is not null
                ? TypedResults.Ok(project)
                : TypedResults.NotFound($"{nameof(Project)} not found with Id ({id})");
        }
    }

    public class GetProjectService(DbContext context) : QueryService<GetProject, Project>(context)
    {
        public override ValueTask<Project> Send(GetProject request) => ValueTask.FromResult(new Project());
    }
}
