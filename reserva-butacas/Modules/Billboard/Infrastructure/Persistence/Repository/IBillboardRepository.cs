using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Modules.Billboard.Domain.Entities;

namespace reserva_butacas.Modules.Billboard.Infrastructure.Persistence.Repository
{
    public interface IBillboardRepository : IBaseRepository<BillboardEntity>
    {
        Task<BillboardEntity> GetByIdWithDetailsAsync(int id);
    }
}