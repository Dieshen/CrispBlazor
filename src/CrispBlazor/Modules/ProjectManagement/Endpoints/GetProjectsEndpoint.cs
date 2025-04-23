using CrispBlazor.Client.Modules.ProjectManagement.Models;
using CrispBlazor.Client.Modules.ProjectManagement.Requests;
using CrispBlazor.Shared.Interfaces;
using CrispBlazor.Shared.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CrispBlazor.Modules.ProjectManagement.Endpoints
{
    public class GetProjectsEndpoint : BaseEndpoint, IGetFilteredEndpoint<GetProjects, Project>
    {
        public static void Map(RouteGroupBuilder builder)
        {
            throw new NotImplementedException();
        }

        public static async Task<IResult> Handle([FromServices] IFilteredQueryService<GetProjects, Project> modelService, [FromServices] IEnumerable<IValidator<GetProjects>> validators, [FromHeader(Name = "query")] string? queryString = null)
        {
            GetProjects query = JsonConvert.DeserializeObject<GetProjects>(queryString ?? "", _jsonSerializerSettings) ?? new();
            await ValidateRequest(query, validators);
            FilteredResponse<Project> response = await modelService.Send(query);
            return TypedResults.Ok(response);
        }
    }

    public class GetProjectsService(DbContext context) : FilteredQueryService<GetProjects, Project>(context)
    {
        public override ValueTask<FilteredResponse<Project>> Send(GetProjects request)
        {
            throw new NotImplementedException();
        }
    }
}
