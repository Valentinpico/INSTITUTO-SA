using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Modules.Seat.Aplication.DTOs
{
    public class SeatsAvailableOccupiedDTO
    {
        public int RoomID { get; set; }

        public int Available { get; set; }
        public int Occupied { get; set; }

        public int Total { get; set; }

    }
}