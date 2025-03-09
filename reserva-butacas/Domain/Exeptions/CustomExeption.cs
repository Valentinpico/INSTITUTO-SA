using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using reserva_butacas.Infrastructure.Api;

namespace reserva_butacas.Domain.Exeptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }
        public List<ErrorModel> Errors { get; } = [];

        public CustomException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }

        public CustomException(string message, List<ErrorModel> errors, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }


}