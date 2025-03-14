using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Modules.Billboard.Aplication.DTOs;
using reserva_butacas.Modules.Billboard.Domain.Entities;
using reserva_butacas.Modules.Customer.Aplication.DTOs;
using reserva_butacas.Modules.Customer.Domain.Entities;
using reserva_butacas.Modules.Seat.Aplication.DTOs;
using reserva_butacas.Modules.Seat.Domain.Entities;

namespace reserva_butacas.Modules.Booking.Aplication.DTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public int CustomerID { get; set; }
        public  CustomerDTO Customer { get; set; }
        public int SeatID { get; set; }
        public  SeatDTO Seat { get; set; }
        public int BillboardID { get; set; }
        public  BillboardDTO Billboard { get; set; }
    }
}