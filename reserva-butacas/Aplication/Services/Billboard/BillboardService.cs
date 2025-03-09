using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Infrastructure.Persistence.Repositories.Billboard;

namespace reserva_butacas.Aplication.Services.Billboard
{
    public class BillboardService(IBillboardRepository billboardRepository) : BaseService<BillboardEntity>(billboardRepository), IBillboardService
    {
        protected readonly IBillboardRepository _billboardRepository = billboardRepository;
    }
}