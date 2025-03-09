using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Modules.Billboard.Aplication.DTOs;
using reserva_butacas.Modules.Billboard.Domain.Entities;
using reserva_butacas.Modules.Billboard.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Billboard.Aplication.Services
{
    public class BillboardService(IBillboardRepository billboardRepository) : BaseService<BillboardEntity>(billboardRepository), IBillboardService
    {
        protected readonly IBillboardRepository _billboardRepository = billboardRepository;
    }
}