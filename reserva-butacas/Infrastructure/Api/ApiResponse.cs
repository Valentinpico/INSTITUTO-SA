using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Infrastructure.Api
{
    public class ErrorModel
    {
        public string? PropertyName { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ErrorCode { get; set; }

    }
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<ErrorModel> Errors { get; set; } = [];
        public required string Message { get; set; }
        public int StatusCode { get; set; }

        // Constructor para respuesta exitosa
        public static ApiResponse<T> SuccessResponse(T? data, string message = "Operation completed successfully")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
                StatusCode = 200
            };
        }

        public static ApiResponse<T> ErrorResponse(List<ErrorModel> errors, string message = "Operation failed", int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Errors = errors,
                Message = message,
                StatusCode = statusCode
            };
        }
    }
}