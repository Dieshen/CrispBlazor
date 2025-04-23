using CrispBlazor.Shared.Models;
using FluentValidation;
using FluentValidation.Validators;
using System.Linq.Dynamic.Core;

namespace CrispBlazor.Shared.Requests
{
    public sealed class FilterExpressionValidator<TQuery, TResponse> : PropertyValidator<TQuery, string?>
        where TQuery : FilteredQuery<TResponse>
        where TResponse : BaseModel
    {
        public override string Name => "FilterExpressionValidator";

        public override bool IsValid(ValidationContext<TQuery> context, string? value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                    DynamicExpressionParser.ParseLambda<TResponse, bool>(ParsingConfig.Default, false, value);
                return true;
            }
            catch (Exception e)
            {
                context.MessageFormatter.AppendArgument("Error", e.Message);
            }
            return false;
        }
        protected override string GetDefaultMessageTemplate(string errorCode) =>
            "{PropertyName} must contain a valid LINQ expression" + Environment.NewLine + "{Error}";
    }

    public sealed class SortExpressionValidator<TQuery, TResponse> : PropertyValidator<TQuery, string?>
        where TQuery : FilteredQuery<TResponse>
        where TResponse : BaseModel
    {
        public override string Name => "SortExpressionValidator";

        public override bool IsValid(ValidationContext<TQuery> context, string? value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                    DynamicExpressionParser.ParseLambda<TResponse, object?>(ParsingConfig.Default, false, value);
                return true;
            }
            catch (Exception e)
            {
                context.MessageFormatter.AppendArgument("Error", e.Message);
            }
            return false;
        }
        protected override string GetDefaultMessageTemplate(string errorCode) =>
            "{PropertyName} must contain a valid LINQ expression" + Environment.NewLine + "{Error}";
    }
}
