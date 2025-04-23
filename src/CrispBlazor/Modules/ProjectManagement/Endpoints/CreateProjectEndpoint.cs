using CrispBlazor.Client.Modules.ProjectManagement.Requests;
using CrispBlazor.Shared.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrispBlazor.Modules.ProjectManagement.Endpoints
{
    public class CreateProjectEndpoint : BaseEndpoint, ICreateEndpoint<CreateProject>
    {
        public static void Map(RouteGroupBuilder builder)
        {
            builder.MapPost("", Handle)
                .Produces<Guid>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        public static async Task<IResult> Handle([FromBody] CreateProject request, [FromServices] ICreateService<CreateProject> modelService, [FromServices] IEnumerable<IValidator<CreateProject>> validators)
        {
            await ValidateRequest(request, validators);
            Guid id = await modelService.Send(request);
            return TypedResults.Created($"/projects/{id}", id);
        }
    }

    public class CreateProjectService(DbContext context) : CreateService<CreateProject>(context)
    {
        public override ValueTask<Guid> Send(CreateProject request)
        {
            return ValueTask.FromResult(Guid.NewGuid());
        }
    }
}
