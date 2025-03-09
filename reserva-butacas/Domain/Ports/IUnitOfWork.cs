using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Domain.Ports
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}