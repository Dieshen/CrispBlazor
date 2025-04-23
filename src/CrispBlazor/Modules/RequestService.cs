using CrispBlazor.Shared.Interfaces;
using CrispBlazor.Shared.Models;
using CrispBlazor.Shared.Requests;
using CrispBlazor.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace CrispBlazor.Modules
{
    public abstract class RequestService<TRequest>(DbContext context) :
        IRequestService<TRequest>
        where TRequest : IRequest
    {
        protected readonly DbContext _context = context;
        public void Dispose()
        {
            _context.Dispose();
        }

        public abstract ValueTask Send(TRequest request);
    }

    public abstract class RequestService<TRequest, TResponse>(DbContext context) :
        IRequestService<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly DbContext _context = context;
        public void Dispose()
        {
            _context.Dispose();
        }

        public abstract ValueTask<TResponse> Send(TRequest request);
    }

    public abstract class CreateService<TCreate>(DbContext context) :
        RequestService<TCreate, Guid>(context),
        ICreateService<TCreate>
        where TCreate : CreateCommand
    {
    }

    public abstract class QueryService<TQuery, TResponse>(DbContext context) :
        RequestService<TQuery, TResponse>(context),
        IQueryService<TQuery, TResponse>
        where TQuery : Query<TResponse>
    {
    }

    public abstract class FilteredQueryService<TQuery, TResponse>(DbContext context) :
        RequestService<TQuery, FilteredResponse<TResponse>>(context),
        IFilteredQueryService<TQuery, TResponse>
        where TQuery : FilteredQuery<TResponse>
        where TResponse : BaseModel
    {

    }
}
