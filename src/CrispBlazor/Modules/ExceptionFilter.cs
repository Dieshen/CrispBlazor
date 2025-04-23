using CrispBlazor.Shared;
using System.ComponentModel;
using System.Net;

namespace CrispBlazor.Modules
{
    public class ExceptionFilter : IEndpointFilter
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger _logger;

        public ExceptionFilter(IWebHostEnvironment environment, ILogger<ExceptionFilter> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            try
            {
                return await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                _logger.LogError(exception.StackTrace);
                IResult result;
                switch (exception)
                {
                    case DomainException domainException:
                    case ArgumentNullException argumentNullException:
                    case InvalidEnumArgumentException invalidEnumArgumentException:
                    case ArgumentOutOfRangeException argumentOutOfRangeException:
                    case ArgumentException argumentException:
                    case NotImplementedException notImplementedException:
                        result = Problem(exception.Message, HttpStatusCode.BadRequest);
                        break;
                    case NotFoundException notFoundException:
                        result = Problem(exception.Message, HttpStatusCode.NotFound);
                        break;
                    case UnauthorizedAccessException unauthorizedAccessException:
                        result = Problem("Access denied", HttpStatusCode.Forbidden);
                        break;
                    default:
                        {
                            string message = "Sorry an error occurred, please try again.";
                            if (!_environment.IsProduction())
                                message = exception.Message;
                            result = Problem(message, HttpStatusCode.InternalServerError);
                        }
                        break;
                }

                return result;
            }
        }

        private IResult Problem(string message, HttpStatusCode statusCode,
            string? title = null, string? instance = null, string? type = null) => Results.Problem(message, instance, (int)statusCode, title, type);
    }

}
