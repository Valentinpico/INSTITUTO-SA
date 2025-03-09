using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Modules.Seat.Aplication.DTOs
{
    public class SeatCancellationDTO
    {
        public int SeatId { get; set; }
        public int BookingId { get; set; }
    }
}