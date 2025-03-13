using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Modules.Room.Domain.Entities;

namespace reserva_butacas.Modules.Room.Infrastructure.Persistence.Repository
{
    public class RoomRepository(AppDbContext context) : BaseRepository<RoomEntity>(context), IRoomRepository
    {
        public new async Task<IEnumerable<RoomEntity>> GetAllAsync()
        {
            return await _dbSet.Include(r => r.Seats).ToListAsync();
        }
        public new async Task<RoomEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(r => r.Seats).FirstOrDefaultAsync(r => r.Id == id);
        }

        public new async Task<IEnumerable<RoomEntity>> SearchAsync(Func<RoomEntity, bool> predicate)
        {
            return await Task.Run(() => _dbSet.Include(r => r.Seats).Where(predicate).ToList());
        }
  
    }
}