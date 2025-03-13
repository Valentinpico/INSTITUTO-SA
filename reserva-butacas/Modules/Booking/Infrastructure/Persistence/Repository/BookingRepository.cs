using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Modules.Booking.Domain.Entities;
using reserva_butacas.Modules.Movie.Domain.Enums;

namespace reserva_butacas.Modules.Booking.Infrastructure.Persistence.Repository
{
    public class BookingRepository(AppDbContext context) : BaseRepository<BookingEntity>(context), IBookingRepository
    {

        public async Task<IEnumerable<BookingEntity>> GetHorrorMovieBookingsInDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Bookings
                .Include(b => b.Billboard)
                .ThenInclude(b => b.Movie)
                .Include(b => b.Customer)
                .Where(b => b.Billboard.Movie.Genre == MovieGenreEnum.HORROR &&
                       b.Date >= startDate && b.Date <= endDate &&
                       b.Status == true)
                .ToListAsync();
        }

        public new async Task<IEnumerable<BookingEntity>> GetAllAsync()
        {
            return await _context.Bookings.Include(r => r.Billboard).Include(r => r.Customer).Include(r => r.Seat).ToListAsync();
        }
        public new async Task<BookingEntity?> GetByIdAsync(int id)
        {
            return await _context.Bookings.Include(r => r.Billboard).Include(r => r.Customer).Include(r => r.Seat).FirstOrDefaultAsync(r => r.Id == id);
        }

        public new async Task<IEnumerable<BookingEntity>> SearchAsync(Func<BookingEntity, bool> predicate)
        {
            return await Task.Run(() => _context.Bookings.Include(r => r.Billboard).Include(r => r.Customer).Include(r => r.Seat).Where(predicate).ToList());
        }
    }
}