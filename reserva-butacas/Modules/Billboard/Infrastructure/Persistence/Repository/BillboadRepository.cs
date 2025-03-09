using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Modules.Billboard.Domain.Entities;

namespace reserva_butacas.Modules.Billboard.Infrastructure.Persistence.Repository
{
    public class BillboardRepository(AppDbContext context) : BaseRepository<BillboardEntity>(context), IBillboardRepository
    {
        public Task<BillboardEntity> GetByIdWithDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}