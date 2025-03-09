using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.Services;

using reserva_butacas.Modules.Customer.Domain.Entities;

namespace reserva_butacas.Modules.Customer.Aplication.Services
{
    public interface ICustomerService : IBaseService<CustomerEntity>
    {

    }
}