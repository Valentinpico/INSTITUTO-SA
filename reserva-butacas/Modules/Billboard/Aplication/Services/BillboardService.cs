using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Validators;
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
        IMapper mapper,
        IUnitOfWork unitOfWork) : IBillboardService
    {
        private readonly IBillboardRepository _billboardRepository = billboardRepository;
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly ISeatRepository _seatRepository = seatRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task AddAsync(BillboardCreateDTO entity)
        {
            var billboard = _mapper.Map<BillboardEntity>(entity);

            await _billboardRepository.AddAsync(billboard);
        }

        public async Task<IEnumerable<CustomerEntity>> CancelBillboardAndBookingsAsync(int id)
        {
            var affectedCustomers = new List<CustomerEntity>();

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var billboard = await _billboardRepository.GetByIdAsync(id)
                    ?? throw new NotFoundException($"Billboard with ID {id} not found");

                if (billboard.Date.Date < DateTime.Today)
                    throw new BadRequestException("No se puede cancelar funciones de la cartelera con fecha anterior a la actual");

                billboard.Status = false;
                await _billboardRepository.UpdateAsync(billboard);

                var bookings = await _bookingRepository.SearchAsync(b => b.BillboardID == id);

                if (bookings == null || !bookings.Any())
                    throw new NotFoundException($"No bookings found for Billboard with ID {id}");

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

        public Task DeleteAsync(int id)
        {
            var billboard = _billboardRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Billboard with ID {id} not found");

            return _billboardRepository.DeleteAsync(id);


        }

        public async Task<IEnumerable<BillboardDTO>> GetAllAsync()
        {

            var billboards = await _billboardRepository.GetAllAsync();

            var billboardsDTO = billboards.Select(b => _mapper.Map<BillboardDTO>(b));

           // billboardsDTO = billboardsDTO.Where(b => b.Status == true);

            return billboardsDTO ?? [];
        }

        public async Task<BillboardDTO> GetByIdAsync(int id)
        {
            var billboard = await _billboardRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Billboard with ID {id} not found");

            if (billboard.Status == false)
                throw new NotFoundException($"Billboard with ID {id} is not active");

            var billboardDTO = _mapper.Map<BillboardDTO>(billboard);

            return billboardDTO;
        }

        public async Task<IEnumerable<BillboardDTO>> SearchAsync(Func<BillboardEntity, bool> predicate)
        {
            var billboards = await _billboardRepository.SearchAsync(predicate);

            var billboardsDTO = billboards.Select(b => _mapper.Map<BillboardDTO>(b));

            billboardsDTO = billboardsDTO.Where(b => b.Status);

            return billboardsDTO ?? [];
        }

        public Task UpdateAsync(BillboardDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(BillboardUpdateDTO entity)
        {
            var billboard = await _billboardRepository.GetByIdAsync(entity.Id)
                ?? throw new NotFoundException($"Billboard with ID {entity.Id} not found in order to update");


            var updatedBillboard = _mapper.Map<BillboardEntity>(entity);

            await _billboardRepository.UpdateAsync(updatedBillboard);
        }


    }
}