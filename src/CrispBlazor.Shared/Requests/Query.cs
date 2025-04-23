using CrispBlazor.Shared.Enums;
using CrispBlazor.Shared.Interfaces;
using CrispBlazor.Shared.Models;
using CrispBlazor.Shared.Responses;

namespace CrispBlazor.Shared.Requests
{
    public abstract record Query<TResponse> : IRequest<TResponse>;
    public abstract record SingularQuery<TResponse> : Query<TResponse>
        where TResponse : BaseModel?
    {
        public SingularQuery(Guid Id) => this.Id = Id;
        public SingularQuery() { }
        public Guid Id { get; init; }
    }

    public abstract record FilteredQuery<TResponse> : Query<FilteredResponse<TResponse>>
        where TResponse : BaseModel
    {
        public bool IncludeArchived { get; init; }
        public string? SortBy { get; init; }
        public bool Descending { get; init; }
        public FilterType FilterType { get; init; }
        public string? Filter { get; init; }
    }

    public abstract record PagedQuery<TResponse> : FilteredQuery<TResponse>
        where TResponse : BaseModel
    {
        public int PageIndex { get; init; } = 0;
        public int PageSize { get; init; } = int.MaxValue;
    }
}
