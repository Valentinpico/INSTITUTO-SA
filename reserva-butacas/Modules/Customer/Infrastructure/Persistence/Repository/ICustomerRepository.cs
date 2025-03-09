using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Modules.Customer.Domain.Entities;

namespace reserva_butacas.Modules.Customer.Infrastructure.Persistence.Repository
{
    public interface ICustomerRepository : IBaseRepository<CustomerEntity>
    {

    }
}