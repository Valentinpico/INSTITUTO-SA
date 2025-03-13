using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Modules.Seat.Aplication.DTOs;
using reserva_butacas.Modules.Seat.Domain.Entities;

namespace reserva_butacas.Modules.Room.Aplication.DTOs
{
    public class RoomDTO
    {
        public required int Id { get; set; }
        public bool Status { get; set; } = true;
        public required string Name { get; set; }
        public short Number { get; set; }

        public required ICollection<SeatDTO> Seats { get; set; }
    }
}