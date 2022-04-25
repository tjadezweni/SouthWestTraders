using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Application.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
               HandleOnAppError(appError);
            });
        }

        private static void HandleOnAppError(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Run(async context =>
            {
                HandleException(context);
            });
        }

        private static async void HandleException(HttpContext context)
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature is null)
            {
                return;
            }
            var exception = contextFeature.Error;
            var statusCode = StatusCodes.Status500InternalServerError;
            var message = "Internal Server Error";
            switch (exception)
            {
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = exception.Message;
                    break;
                case FoundException:
                    statusCode = StatusCodes.Status409Conflict;
                    message = exception.Message;
                    break;
                case ApiException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = exception.Message;
                    break;
                default:
                    break;
            }
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var responseText = new ErrorDetails()
            {
                Message = message
            }.ToString();
            await context.Response.WriteAsync(responseText);
        }

        public class ErrorDetails
        {
            public string Message { get; set; } = null!;

            public override string ToString()
            {
                return "{ \"message\": \"" + Message + "\"}";
            }
        }
    }

    

    
}
