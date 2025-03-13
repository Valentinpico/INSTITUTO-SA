using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Modules.Room.Domain.Entities;

namespace reserva_butacas.Modules.Room.Infrastructure.Persistence.Repository
{
    public interface IRoomRepository : IBaseRepository<RoomEntity>
    {

        new public Task<IEnumerable<RoomEntity>> GetAllAsync();
        new public Task<RoomEntity?> GetByIdAsync(int id);

       new public Task<IEnumerable<RoomEntity>> SearchAsync(Func<RoomEntity, bool> predicate);



    }
}