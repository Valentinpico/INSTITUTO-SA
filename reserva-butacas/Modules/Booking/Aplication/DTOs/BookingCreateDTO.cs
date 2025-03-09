using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Modules.Booking.Aplication.DTOs
{
    public class BookingCreateDTO
    {

        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public int CustomerID { get; set; }
        public int SeatID { get; set; }
        public int BillboardID { get; set; }
    }
}