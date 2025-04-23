using CrispBlazor.Shared;
using CrispBlazor.Shared.Interfaces;
using CrispBlazor.Shared.Models;
using CrispBlazor.Shared.Requests;
using CrispBlazor.Shared.Responses;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CrispBlazor.Modules
{
    public abstract class BaseEndpoint
    {
        protected static readonly JsonSerializerSettings _jsonSerializerSettings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
            {
                IgnoreSerializableInterface = true,
                IgnoreSerializableAttribute = true
            },
        };

        public static async Task ValidateRequest<TRequest>(TRequest request, IEnumerable<IValidator<TRequest>> validators)
           where TRequest : IBaseRequest
        {
            // Check if there are any validators
            if (validators.Any())
            {
                ValidationContext<TRequest> context = new(request);

                // Validate the request asynchronously
                ValidationResult[] validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context)));

                // Collect all validation failures
                List<ValidationFailure> failures = validationResults.Where(r => r.Errors.Any()).SelectMany(r => r.Errors).ToList();

                // If there are any failures, throw a DomainException with the error message
                if (failures.Any()) throw new DomainException(BuildErrorMessage(failures));
            }
        }

        public static string BuildErrorMessage(IEnumerable<ValidationFailure> errors)
        {
            IEnumerable<string> arr = errors.Select(x => $"{Environment.NewLine} -- {x.PropertyName}: {x.ErrorMessage} Severity: {x.Severity}");
            return "Validation failed: " + string.Join(string.Empty, arr);
        }
    }
    public interface IEndpoint
    {
        static abstract void Map(RouteGroupBuilder builder);
    }

    public interface ICreateEndpoint<TCreate> : IEndpoint
        where TCreate : CreateCommand
    {
        static abstract Task<IResult> Handle([FromBody] TCreate request, [FromServices] ICreateService<TCreate> modelService, [FromServices] IEnumerable<IValidator<TCreate>> validators);
    }

    public interface ICreateEndpoint<TCreate, TModelService> : IEndpoint
        where TCreate : CreateCommand
        where TModelService : ICreateService<TCreate>
    {
        static abstract Task<IResult> Handle([FromBody] TCreate request, [FromServices] TModelService modelService, [FromServices] IEnumerable<IValidator<TCreate>> validators);
    }

    public interface IGetByIdEndpoint<TRequest, TResponse> : IEndpoint
        where TRequest : SingularQuery<TResponse>
        where TResponse : BaseModel
    {
        static abstract Task<IResult> Handle([FromRoute] Guid id, [FromServices] IRequestService<TRequest, TResponse> modelService, [FromServices] IEnumerable<IValidator<TRequest>> validators);

    }

    public interface IGetByIdEndpoint<TRequest, TResponse, in TModelService> : IEndpoint
        where TRequest : SingularQuery<TResponse>
        where TResponse : BaseModel
        where TModelService : IRequestService<TRequest, TResponse>
    {
        static abstract Task<IResult> Handle([FromRoute] Guid id, [FromServices] TModelService modelService, [FromServices] IEnumerable<IValidator<TRequest>> validators);
    }

    public interface IGetFilteredEndpoint<TRequest, TResponse> : IEndpoint
         where TRequest : FilteredQuery<TResponse>
        where TResponse : BaseModel
    {
        static abstract Task<IResult> Handle([FromServices] IFilteredQueryService<TRequest, TResponse> modelService, [FromServices] IEnumerable<IValidator<TRequest>> validators, [FromHeader(Name = "query")] string? queryString = null);
    }

    public interface IGetFilteredEndpoint<TRequest, TResponse, in TModelService> : IEndpoint
        where TRequest : FilteredQuery<TResponse>
        where TResponse : BaseModel
        where TModelService : IRequestService<TRequest, FilteredResponse<TResponse>>
    {
        static abstract Task<IResult> Handle([FromServices] TModelService modelService, [FromServices] IEnumerable<IValidator<TRequest>> validators, [FromHeader(Name = "query")] string? queryString = null);
    }

    public interface IModifyEndpoint<TModify> : IEndpoint
        where TModify : ModifyCommand
    {
        static abstract Task<IResult> Handle([FromBody] TModify request, [FromServices] IRequestService<TModify> modelService, [FromServices] IEnumerable<IValidator<TModify>> validators);
    }

    public interface IModifyEndpoint<TModify, in TModelService> : IEndpoint
        where TModify : ModifyCommand
        where TModelService : IRequestService<TModify>
    {
        static abstract Task<IResult> Handle([FromBody] TModify request, [FromServices] TModelService modelService, [FromServices] IEnumerable<IValidator<TModify>> validators);
    }

    public interface IFromRouteModifyEndpoint<TModify> : IEndpoint
        where TModify : ModifyCommand
    {
        static abstract Task<IResult> Handle([FromRoute] Guid id, [FromServices] IRequestService<TModify> modelService, [FromServices] IEnumerable<IValidator<TModify>> validators);
    }

    public interface IFromRouteModifyEndpoint<TModify, in TModelService> : IEndpoint
        where TModify : ModifyCommand
        where TModelService : IRequestService<TModify>
    {
        static abstract Task<IResult> Handle([FromRoute] Guid id, [FromServices] TModelService modelService, [FromServices] IEnumerable<IValidator<TModify>> validators);
    }
}