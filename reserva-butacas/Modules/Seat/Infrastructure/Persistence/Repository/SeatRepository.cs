using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Modules.Seat.Aplication.DTOs;
using reserva_butacas.Modules.Seat.Domain.Entities;

namespace reserva_butacas.Modules.Seat.Infrastructure.Persistence.Repository
{
    public class SeatRepository(AppDbContext context) : BaseRepository<SeatEntity>(context), ISeatRepository
    {

        public async Task<IEnumerable<SeatsAvailableOccupiedDTO>> GetSeatAvailabilityByRoomForToday()
        {
            var today = DateTime.Today;

            List<SeatsAvailableOccupiedDTO> result = [];

            var rooms = await _context.Rooms.Where(r => r.Status).ToListAsync();

            foreach (var room in rooms)
            {
                var totalSeats = await _context.Seats
                    .Where(s => s.RoomID == room.Id && s.Status)
                    .CountAsync();

                var billboards = await _context.Billboards
                    .Where(b => b.Date.Date == today && b.RoomID == room.Id && b.Status)
                    .ToListAsync();

                var billboardIds = billboards.Select(b => b.Id).ToList();

                var occupiedSeats = await _context.Bookings
                    .Where(b => billboardIds.Contains(b.BillboardID) && b.Status)
                    .Select(b => b.SeatID)
                    .Distinct()
                    .CountAsync();

                var availableSeats = totalSeats - occupiedSeats;

                result.Add(new SeatsAvailableOccupiedDTO
                {
                    RoomID = room.Id,
                    Available = availableSeats,
                    Occupied = occupiedSeats,
                    Total = totalSeats

                });
            }

            return result;
        }
    }
}