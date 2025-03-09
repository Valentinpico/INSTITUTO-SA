using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Modules.Booking.Aplication.DTOs;
using reserva_butacas.Modules.Booking.Domain.Entities;

namespace reserva_butacas.Modules.Booking.Aplication.Services
{
    public interface IBookingService : IBaseService<BookingEntity>
    {

    }
}