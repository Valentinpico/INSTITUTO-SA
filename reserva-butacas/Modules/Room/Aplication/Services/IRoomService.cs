using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Modules.Room.Aplication.DTOs;
using reserva_butacas.Modules.Room.Domain.Entities;

namespace reserva_butacas.Modules.Room.Aplication.Services
{
    public interface IRoomService : IBaseService<RoomEntity, RoomDTO, RoomCreatedDTO>
    {

    }
}