using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Domain.Ports;
using reserva_butacas.Modules.Billboard.Aplication.DTOs;
using reserva_butacas.Modules.Billboard.Domain.Entities;
using reserva_butacas.Modules.Billboard.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Booking.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Customer.Domain.Entities;
using reserva_butacas.Modules.Seat.Aplication.DTOs;
using reserva_butacas.Modules.Seat.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Billboard.Aplication.Services
{
    public class BillboardService(
        IBillboardRepository billboardRepository,
        IBookingRepository bookingRepository,
        ISeatRepository seatRepository,
        IUnitOfWork unitOfWork) : BaseService<BillboardEntity>(billboardRepository), IBillboardService
    {
        private readonly IBillboardRepository _billboardRepository = billboardRepository;
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly ISeatRepository _seatRepository = seatRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<CustomerEntity>> CancelBillboardAndBookingsAsync(BillboardCancellationDTO dto)
        {
            var affectedCustomers = new List<CustomerEntity>();

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var billboard = await _billboardRepository.GetByIdAsync(dto.BillboardId)
                    ?? throw new NotFoundException($"Billboard with ID {dto.BillboardId} not found");

                if (billboard.Date.Date < DateTime.Today)
                    throw new BadRequestException("No se puede cancelar funciones de la cartelera con fecha anterior a la actual");

                billboard.Status = false;
                await _billboardRepository.UpdateAsync(billboard);

                var bookings = await _bookingRepository.SearchAsync(b => b.BillboardID == dto.BillboardId);
                if (bookings == null || !bookings.Any())
                    throw new NotFoundException($"No bookings found for Billboard with ID {dto.BillboardId}");

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

        public Task CancelSeatAndBookingAsync(SeatCancellationDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}