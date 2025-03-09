using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Modules.Room.Domain.Entities;
using reserva_butacas.Modules.Room.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Room.Aplication.Services
{
    public class RoomService(IRoomRepository roomRepository) : BaseService<RoomEntity>(roomRepository), IRoomService
    {
        private readonly IRoomRepository _roomRepository = roomRepository;

    }
}