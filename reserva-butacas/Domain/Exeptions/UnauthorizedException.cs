using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Domain.Exeptions
{
    public class UnauthorizedException(string message) : CustomException(message, 401)
    {
    }
}