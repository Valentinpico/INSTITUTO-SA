using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Domain.Exeptions;

namespace reserva_butacas.Infrastructure.Api.Middlewares
{
    public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = new
                {
                    message = exception.Message,
                    detail = exception.InnerException?.Message
                }
            };

            switch (exception)
            {
                case CartelleraCancelacionException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case KeyNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}