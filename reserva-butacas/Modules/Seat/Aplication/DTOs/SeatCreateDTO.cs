using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Modules.Seat.Aplication.DTOs
{
    public class SeatCreateDTO
    {
        public short Number { get; set; }
        public short RowNumber { get; set; }
        public int RoomID { get; set; }
    }
}