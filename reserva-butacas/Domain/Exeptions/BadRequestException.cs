using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Infrastructure.Api;

namespace reserva_butacas.Domain.Exeptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message) : base(message, 400)
        {
        }

        public BadRequestException(string message, List<ErrorModel> errors) : base(message, errors, 400)
        {
        }
    }
}