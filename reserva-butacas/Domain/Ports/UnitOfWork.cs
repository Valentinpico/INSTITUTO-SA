using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Domain.Ports
{
    public class UnitOfWork : IUnitOfWork
    {
        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task RollbackAsync()
        {
            throw new NotImplementedException();
        }
    }
}