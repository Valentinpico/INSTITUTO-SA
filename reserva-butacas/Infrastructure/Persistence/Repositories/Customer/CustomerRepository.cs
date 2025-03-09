using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Domain.Entities;

namespace reserva_butacas.Infrastructure.Persistence.Repositories.Billboard
{
    public class CustomerRepository(AppDbContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
    {
    }
}