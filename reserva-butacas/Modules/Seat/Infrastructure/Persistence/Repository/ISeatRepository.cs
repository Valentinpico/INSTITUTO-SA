using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Modules.Seat.Domain.Entities;

namespace reserva_butacas.Modules.Seat.Infrastructure.Persistence.Repository
{
    public interface ISeatRepository : IBaseRepository<SeatEntity>
    {
        Task<Dictionary<int, (int Available, int Occupied)>> GetSeatAvailabilityByRoomForToday();
        new Task<SeatEntity> GetByIdAsync(int id);
        new Task UpdateAsync(SeatEntity seat);
    }
}