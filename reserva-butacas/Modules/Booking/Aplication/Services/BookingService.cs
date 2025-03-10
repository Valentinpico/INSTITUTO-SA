using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Modules.Billboard.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Booking.Aplication.DTOs;
using reserva_butacas.Modules.Booking.Domain.Entities;
using reserva_butacas.Modules.Booking.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Customer.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Seat.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Booking.Aplication.Services
{
    public class BookingService(
        IBookingRepository bookingRepository,
        ICustomerRepository customerRepository,
        ISeatRepository seatRepository,
        IBillboardRepository billboardRepository
        ) : BaseService<BookingEntity>(bookingRepository), IBookingService
    {
        protected readonly IBookingRepository _baseService = bookingRepository;
        protected readonly ICustomerRepository _customerRepository = customerRepository;
        protected readonly ISeatRepository _seatRepository = seatRepository;
        protected readonly IBillboardRepository _billboardRepository = billboardRepository;


        new public async Task AddAsync(BookingEntity entity)
        {
            var customerExist = await _customerRepository.GetByIdAsync(entity.CustomerID)
            ?? throw new NotFoundException("Customer not found");

            var seatExist = await _seatRepository.GetByIdAsync(entity.SeatID)
            ?? throw new NotFoundException("seat not found");

            var billboardrExist = await _billboardRepository.GetByIdAsync(entity.BillboardID)
            ?? throw new NotFoundException("billborad nots found");


            await _baseService.AddAsync(entity);
        }

        new public async Task<IEnumerable<BookingEntity>> GetAllAsync()
        {
            var bookings = await _baseService.GetAllAsync() ?? [];

            var bookingsTrue = bookings.Where(x => x.Status);

            return bookingsTrue;

        }
    }
}