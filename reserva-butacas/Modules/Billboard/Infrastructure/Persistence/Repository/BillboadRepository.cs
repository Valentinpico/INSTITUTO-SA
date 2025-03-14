using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Modules.Billboard.Domain.Entities;

namespace reserva_butacas.Modules.Billboard.Infrastructure.Persistence.Repository
{
    public class BillboardRepository(AppDbContext context) : BaseRepository<BillboardEntity>(context), IBillboardRepository
    {
        public Task<BillboardEntity> GetByIdWithDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

         public new async Task<IEnumerable<BillboardEntity>> GetAllAsync()
        {
            return await _context.Billboards.Include(r => r.Movie).Include(r => r.Room.Seats).ToListAsync();
        }
        public new async Task<BillboardEntity?> GetByIdAsync(int id)
        {
            return await _context.Billboards.Include(r => r.Movie).Include(r => r.Room.Seats).FirstOrDefaultAsync(r => r.Id == id);
        }

        public new async Task<IEnumerable<BillboardEntity>> SearchAsync(Func<BillboardEntity, bool> predicate)
        {
            return await Task.Run(() => _context.Billboards.Include(r => r.Movie).Include(r => r.Room.Seats).Where(predicate).ToList());
        }
    }
}