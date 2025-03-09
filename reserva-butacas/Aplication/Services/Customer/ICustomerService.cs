using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Infrastructure.Persistence.Repositories;

namespace reserva_butacas.Aplication.Services.Customer
{
    public interface ICustomerService : IBaseService<CustomerEntity>
    {

    }
}