using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Modules.Seat.Domain.Entities;

namespace reserva_butacas.Modules.Seat.Infrastructure.Persistence.Repository
{
    public class SeatRepository(AppDbContext context) : BaseRepository<SeatEntity>(context), ISeatRepository
    {

        public async Task<Dictionary<int, (int Available, int Occupied)>> GetSeatAvailabilityByRoomForToday()
        {
            var today = DateTime.Today;

            var result = new Dictionary<int, (int Available, int Occupied)>();

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

                result.Add(room.Id, (availableSeats, occupiedSeats));
            }

            return result;
        }
    }
}