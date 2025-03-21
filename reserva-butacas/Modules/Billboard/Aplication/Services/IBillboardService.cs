using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Modules.Billboard.Aplication.DTOs;
using reserva_butacas.Modules.Billboard.Domain.Entities;
using reserva_butacas.Modules.Customer.Domain.Entities;
using reserva_butacas.Modules.Seat.Aplication.DTOs;

namespace reserva_butacas.Modules.Billboard.Aplication.Services
{
    public interface IBillboardService : IBaseService<BillboardEntity, BillboardDTO, BillboardCreateDTO>
    {
        Task<IEnumerable<CustomerEntity>> CancelBillboardAndBookingsAsync(int id);

        Task CancelSeatAndBookingAsync(SeatCancellationDTO dto);
        Task UpdateAsync(BillboardUpdateDTO billboard);

    }
}