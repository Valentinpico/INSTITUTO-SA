using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Modules.Seat.Domain.Entities;
using reserva_butacas.Modules.Seat.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Seat.Aplication.Services
{
    public class SeatService(ISeatRepository seatRepository) : BaseService<SeatEntity>(seatRepository), ISeatService
    {

        private readonly ISeatRepository _seatRepository = seatRepository;

    }
}