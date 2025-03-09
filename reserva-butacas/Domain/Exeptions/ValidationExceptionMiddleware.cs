using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Infrastructure.Api;

namespace reserva_butacas.Domain.Exeptions
{
    public class ValidationExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            ApiResponse<object> response;

            if (exception is CustomException customException)
            {
                response = ApiResponse<object>.ErrorResponse(
                    customException.Errors,
                    customException.Message,
                    customException.StatusCode);

                context.Response.StatusCode = customException.StatusCode;
            }
            else
            {
                var errors = new List<ErrorModel>
            {
                new() { PropertyName = string.Empty, ErrorMessage = exception.Message }
            };

                response = ApiResponse<object>.ErrorResponse(
                    errors,
                    "Internal Server Error",
                    500);

                context.Response.StatusCode = 500;
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}