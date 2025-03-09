using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using reserva_butacas.Infrastructure.Api;

namespace reserva_butacas.Domain.Exeptions
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger;

        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var apiResponse = exception switch
            {
                CustomException customException => CreateErrorResponse(customException),
                // Puedes manejar otros tipos de excepciones aquí
                _ => CreateUnexpectedErrorResponse(exception)
            };

            httpContext.Response.StatusCode = apiResponse.StatusCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(apiResponse, cancellationToken);

            return true;
        }

        private static ApiResponse<object> CreateErrorResponse(CustomException exception)
        {
            return ApiResponse<object>.ErrorResponse(
                exception.Errors,
                exception.Message,
                exception.StatusCode);
        }

        private ApiResponse<object> CreateUnexpectedErrorResponse(Exception exception)
        {
            _logger.LogError(exception, "Unexpected error occurred");

            var error = new ErrorModel { PropertyName = string.Empty, ErrorMessage = "An unexpected error occurred" };

            return ApiResponse<object>.ErrorResponse(
                [error],
                "Internal Server Error",
                500);
        }
    }

}