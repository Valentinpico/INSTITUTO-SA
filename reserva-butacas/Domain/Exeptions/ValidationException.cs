using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Infrastructure.Api;

namespace reserva_butacas.Domain.Exeptions
{

    public class ValidationException(string message, List<ErrorModel> errors) : CustomException(message, errors, 400)
    {

    }
}