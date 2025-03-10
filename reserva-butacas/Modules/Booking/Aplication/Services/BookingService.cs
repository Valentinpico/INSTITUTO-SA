using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        IBillboardRepository billboardRepository,
        IMapper mapper
        ) : IBookingService
    {
        protected readonly IBookingRepository _bookingRepository = bookingRepository;
        protected readonly ICustomerRepository _customerRepository = customerRepository;
        protected readonly ISeatRepository _seatRepository = seatRepository;
        protected readonly IBillboardRepository _billboardRepository = billboardRepository;
        protected readonly IMapper _mapper = mapper;

        public async Task AddAsync(BookingCreateDTO entity)
        {
            var customer = await _customerRepository.GetByIdAsync(entity.CustomerID)
                ?? throw new NotFoundException("Customer not found");

            var seat = await _seatRepository.GetByIdAsync(entity.SeatID)
                ?? throw new NotFoundException("Seat not found");

            var billboard = await _billboardRepository.GetByIdAsync(entity.BillboardID)
                ?? throw new NotFoundException("Billboard not found");

            var booking = _mapper.Map<BookingEntity>(entity);

            await _bookingRepository.AddAsync(booking);
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Booking not found");

            await _bookingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookingDTO>> GetAllAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();

            bookings = bookings.Where(b => b.Status);

            return _mapper.Map<IEnumerable<BookingDTO>>(bookings) ?? [];
        }

        public async Task<BookingDTO> GetByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Booking not found");

            return _mapper.Map<BookingDTO>(booking);
        }

        public async Task<IEnumerable<BookingDTO>> SearchAsync(Func<BookingEntity, bool> predicate)
        {
            var bookings = await _bookingRepository.SearchAsync(predicate);

            bookings = bookings.Where(b => b.Status);

            return _mapper.Map<IEnumerable<BookingDTO>>(bookings) ?? [];

        }

        public async Task UpdateAsync(BookingDTO entity)
        {
            var booking = await _bookingRepository.GetByIdAsync(entity.Id)
                ?? throw new NotFoundException("Booking not found");

            var customer = await _customerRepository.GetByIdAsync(entity.CustomerID)
                ?? throw new NotFoundException("Customer not found");

            var seat = await _seatRepository.GetByIdAsync(entity.SeatID)
                ?? throw new NotFoundException("Seat not found");

            var billboard = await _billboardRepository.GetByIdAsync(entity.BillboardID)
                ?? throw new NotFoundException("Billboard not found");

            var bookingUpdated = _mapper.Map<BookingEntity>(entity);

            await _bookingRepository.UpdateAsync(bookingUpdated);
        }
    }
}