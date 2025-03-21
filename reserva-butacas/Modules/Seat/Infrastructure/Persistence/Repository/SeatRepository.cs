using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Modules.Room.Aplication.DTOs;
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

            var billboards = await _context.Billboards
                    .Where(b => b.Date.Date == today && b.Status).Include(b => b.Room)
                    .ToListAsync();

            var billboardIds = billboards.Select(b => b.Id).ToList();

            foreach (var billboard in billboards)
            {
                var totalSeats = await _context.Seats
                .Where(s => s.RoomID == billboard.RoomID && s.Status).ToListAsync();

                var bookings = await _context.Bookings
                    .Where(b => b.BillboardID == billboard.Id && b.Status)
                    .ToListAsync();

                var occupiedSeats = bookings.Count;

                var availableSeats = totalSeats.Count - occupiedSeats;

                result.Add(new SeatsAvailableOccupiedDTO
                {
                    RoomID = billboard.RoomID,
                    Available = availableSeats,
                    Occupied = occupiedSeats,
                    Total = totalSeats.Count,
                    Room = new RoomDTO
                    {
                        Id = billboard.Room.Id,
                        Name = billboard.Room.Name,
                        Number = billboard.Room.Number,
                        Seats = []
                    }

                });
            }



            return result;
        }
    }
}