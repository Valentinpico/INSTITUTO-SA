using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Modules.Booking.Aplication.DTOs;
using reserva_butacas.Modules.Booking.Domain.Entities;
using reserva_butacas.Modules.Booking.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Booking.Aplication.Services
{
    public class BookingService(IBookingRepository bookingRepository) : BaseService<BookingEntity>(bookingRepository), IBookingService
    {
        protected readonly IBookingRepository _baseService = bookingRepository;
    }
}