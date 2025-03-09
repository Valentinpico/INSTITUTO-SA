using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Infrastructure.Api;

namespace reserva_butacas.Domain.Exeptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(message, 404)
        {
        }

        public NotFoundException(string message, List<ErrorModel> errors) : base(message, errors, 404)
        {
        }
    }
}