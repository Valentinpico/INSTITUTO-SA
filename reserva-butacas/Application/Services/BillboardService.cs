using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Application.DTOs.Billboard;
using reserva_butacas.Application.DTOs.Seat;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Domain.Exceptions;
using reserva_butacas.Domain.Ports;

namespace reserva_butacas.Application.Services
{
    public class BillboardService(IUnitOfWork _unitOfWork, IBillboardRepository _billboardRepository,
                                    IBookingRepository _bookingRepository, ISeatRepository _seatRepository)
    {
        private readonly IUnitOfWork _unitOfWork = _unitOfWork;
        private readonly IBillboardRepository _billboardRepository = _billboardRepository;
        private readonly IBookingRepository _bookingRepository = _bookingRepository;
        private readonly ISeatRepository _seatRepository = _seatRepository;


        public async Task<List<CustomerEntity>> CancelBillboardAndBookingsAsync(BillboardCancellationDTO dto)
        {
            var affectedCustomers = new List<CustomerEntity>();

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var billboard = await _billboardRepository.GetByIdWithDetailsAsync(dto.BillboardID)
                                ?? throw new KeyNotFoundException($"Billboard with ID {dto.BillboardID} not found");

                if (billboard.Date.Date < DateTime.Today)
                    throw new CartelleraCancelacionException("No se puede cancelar funciones de la cartelera con fecha anterior a la actual");

                billboard.Status = false;
                await _billboardRepository.UpdateAsync(billboard);

                var bookings = await _bookingRepository.GetAllByBillboardIdAsync(dto.BillboardID);

                foreach (var booking in bookings)
                {
                    if (booking.Customer != null && !affectedCustomers.Any(c => c.Id == booking.Customer.Id))
                        affectedCustomers.Add(booking.Customer);

                    booking.Status = false;
                    await _bookingRepository.UpdateAsync(booking);

                    var seat = await _seatRepository.GetByIdAsync(booking.SeatID);
                    if (seat != null)
                    {
                        seat.Status = true;
                        await _seatRepository.UpdateAsync(seat);
                    }
                }

                await _unitOfWork.CommitAsync();

                Console.WriteLine("Clientes afectados por cancelación de cartelera:");
                foreach (var customer in affectedCustomers)
                {
                    Console.WriteLine($"- {customer.Name} {customer.Lastname} (Doc: {customer.DocumentNumber})");
                }

                return affectedCustomers;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task CancelSeatAndBookingAsync(SeatCancellationDTO dto)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var booking = await _bookingRepository.GetBySeatIdAsync(dto.SeatID)
                                ?? throw new KeyNotFoundException($"Booking with seat ID {dto.SeatID} not found");

                if (booking.Billboard.Date.Date < DateTime.Today)
                    throw new ButacaCancelacionException("No se puede cancelar butacas de funciones con fecha anterior a la actual");

                booking.Status = false;
                await _bookingRepository.UpdateAsync(booking);

                var seat = await _seatRepository.GetByIdAsync(dto.SeatID);
                if (seat != null)
                {
                    seat.Status = true;
                    await _seatRepository.UpdateAsync(seat);
                }

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