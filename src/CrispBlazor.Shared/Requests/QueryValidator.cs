using CrispBlazor.Shared.Enums;
using CrispBlazor.Shared.Models;
using CrispBlazor.Shared.Responses;
using FluentValidation;

namespace CrispBlazor.Shared.Requests
{
    internal abstract class QueryValidator<TQuery, TResponse> : AbstractValidator<TQuery>
        where TQuery : Query<TResponse>
    {
    }

    internal abstract class SingularQueryValidator<TQuery, TResponse> : QueryValidator<TQuery, TResponse>
        where TQuery : SingularQuery<TResponse>
        where TResponse : BaseModel
    {
        public SingularQueryValidator() => RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
    }

    internal abstract class FilteredQueryValidator<TQuery, TResponse> : QueryValidator<TQuery, FilteredResponse<TResponse>>
        where TQuery : FilteredQuery<TResponse>
        where TResponse : BaseModel
    {
    }

    internal abstract class PagedQueryValidator<TQuery, TResponse> : FilteredQueryValidator<TQuery, TResponse>
        where TQuery : PagedQuery<TResponse>
        where TResponse : BaseModel
    {
        public PagedQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Page must be greater than or equal to 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("PageSize must be greater than 0.");

            When(x => !string.IsNullOrEmpty(x.Filter), () =>
            {
                When(x => x.FilterType == FilterType.Expression, () =>
                    {
                        RuleFor(x => x.Filter)
                        .SetValidator(new FilterExpressionValidator<TQuery, TResponse>());
                    })
                .Otherwise(() =>
                {
                    RuleFor(x => x.Filter)
                        .NotEmpty();

                });
            });

            When(x => !string.IsNullOrEmpty(x.SortBy), () =>
            {
                RuleFor(x => x.SortBy)
                    .SetValidator(new SortExpressionValidator<TQuery, TResponse>());
            });
        }
    }
}
