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
    public class BillboardRepository : BaseRepository<BillboardEntity>, IBillboardRepository
    {


        public BillboardRepository(AppDbContext context) : base(context)
        {
        }

    }
}