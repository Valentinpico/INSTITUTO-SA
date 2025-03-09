using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Domain.Ports;
using reserva_butacas.Modules.Booking.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Seat.Aplication.DTOs;
using reserva_butacas.Modules.Seat.Domain.Entities;
using reserva_butacas.Modules.Seat.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Seat.Aplication.Services
{
    public class SeatService(
        ISeatRepository seatRepository,
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork)
        : BaseService<SeatEntity>(seatRepository), ISeatService
    {

        private readonly ISeatRepository _seatRepository = seatRepository;
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task CancelSeatAndBookingAsync(SeatCancellationDTO dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var seat = await _seatRepository.GetByIdAsync(dto.SeatId)
                    ?? throw new KeyNotFoundException($"Seat with ID {dto.SeatId} not found");


                seat.Status = false;
                await _seatRepository.UpdateAsync(seat);

                var booking = await _bookingRepository.GetByIdAsync(dto.BookingId)
                    ?? throw new KeyNotFoundException($"Booking with ID {dto.BookingId} not found");

                booking.Status = false;
                await _bookingRepository.UpdateAsync(booking);

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}