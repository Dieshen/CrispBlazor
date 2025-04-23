using CrispBlazor.Shared.Models;
using CrispBlazor.Shared.Requests;
using CrispBlazor.Shared.Responses;

namespace CrispBlazor.Shared.Interfaces
{
    public interface IRequestService<TRequest> : IDisposable
        where TRequest : IRequest
    {
        /// <summary>
        /// Executes the request and returns a response
        /// </summary>
        /// <param name="request"><see cref="TRequest"/></param>
        /// <exception cref="DomainException"></exception>
        ValueTask Send(TRequest request);
    }

    public interface IRequestService<TRequest, TResponse> : IDisposable
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Executes the request and returns a response
        /// </summary>
        /// <param name="request"><see cref="TRequest"/></param>
        /// <returns><see cref="TResponse"/></returns>
        /// <exception cref="DomainException"></exception>
        ValueTask<TResponse> Send(TRequest request);
    }

    public interface ICreateService<TCreate> : IRequestService<TCreate, Guid>
        where TCreate : CreateCommand
    {
    }

    public interface IQueryService<TQuery, TResponse> : IRequestService<TQuery, TResponse>
        where TQuery : Query<TResponse>
    {
    }

    public interface IFilteredQueryService<TQuery, TResponse> : IRequestService<TQuery, FilteredResponse<TResponse>>
        where TQuery : FilteredQuery<TResponse>
        where TResponse : BaseModel
    {

    }
}
