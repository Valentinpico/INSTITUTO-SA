using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Domain.Ports;
using reserva_butacas.Modules.Booking.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Room.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Seat.Aplication.DTOs;
using reserva_butacas.Modules.Seat.Domain.Entities;
using reserva_butacas.Modules.Seat.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Seat.Aplication.Services
{
    public class SeatService(
        ISeatRepository seatRepository,
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IRoomRepository roomRepository)
        : ISeatService
    {

        private readonly ISeatRepository _seatRepository = seatRepository;
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IRoomRepository _roomRepository = roomRepository;

        public async Task AddAsync(SeatCreateDTO entity)
        {
            var roomExists = await _roomRepository.GetByIdAsync(entity.RoomID)
                ?? throw new NotFoundException($"Room with ID {entity.RoomID} not found"); ;

            var rowNumberExist = await _seatRepository.SearchAsync(x => x.RowNumber == entity.RowNumber && x.RoomID == entity.RoomID && x.Number == entity.Number);

            if (rowNumberExist.Any())
            {
                throw new BadRequestException($"The seat {entity.Number} in row {entity.RowNumber} already exists in the room {roomExists.Name}");
            }

            var seat = _mapper.Map<SeatEntity>(entity);

            await _seatRepository.AddAsync(seat);

        }

        public async Task CancelSeatAndBookingAsync(int idSeat)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var seat = await _seatRepository.GetByIdAsync(idSeat)
                    ?? throw new NotFoundException($"Seat with ID {idSeat} not found");

                seat.Status = false;

                await _seatRepository.UpdateAsync(seat);

                var today = DateTime.Today;

                var listBookings = await _bookingRepository.SearchAsync(x => x.SeatID == idSeat
                    && x.Status
                    && x.Billboard.Date.Date >= today);


                foreach (var booking in listBookings)
                {
                    booking.Status = false;
                    Console.WriteLine("Customer affected: " + booking.CustomerID);
                    await _bookingRepository.UpdateAsync(booking);
                }

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public Task DeleteAsync(int id)
        {
            var seatExist = _seatRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Seat with ID {id} not found");

            return _seatRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SeatDTO>> GetAllAsync()
        {
            var seats = await _seatRepository.GetAllAsync();

            //seats = seats.Where(x => x.Status);

            return _mapper.Map<IEnumerable<SeatDTO>>(seats);
        }

        public async Task<SeatDTO> GetByIdAsync(int id)
        {
            var seat = await _seatRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Seat with ID {id} not found");

            return _mapper.Map<SeatDTO>(seat);
        }

        public async Task<IEnumerable<SeatDTO>> SearchAsync(Func<SeatEntity, bool> predicate)
        {
            var seats = await _seatRepository.SearchAsync(predicate);

            seats = seats.Where(x => x.Status);

            return _mapper.Map<IEnumerable<SeatDTO>>(seats.Where(predicate));
        }

        public async Task UpdateAsync(SeatDTO entity)
        {
            var seat = await _seatRepository.GetByIdAsync(entity.Id)
                ?? throw new NotFoundException($"Seat with ID {entity.Id} not found");

            var roomExists = await _roomRepository.GetByIdAsync(entity.RoomID)
                ?? throw new NotFoundException($"Room with ID {entity.RoomID} not found");

            var rowNumberExist = await _seatRepository.SearchAsync(x => x.RowNumber == entity.RowNumber && x.RoomID == entity.RoomID && x.Number == entity.Number && x.Id != entity.Id);

            if (rowNumberExist.Any())
            {
                throw new BadRequestException($"The seat {entity.Number} in row {entity.RowNumber} already exists in the room {roomExists.Name}");
            }


            seat = _mapper.Map<SeatEntity>(entity);

            await _seatRepository.UpdateAsync(seat);
        }
    }
}