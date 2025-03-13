using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Modules.Movie.Aplication.DTOs;
using reserva_butacas.Modules.Room.Aplication.DTOs;

namespace reserva_butacas.Modules.Billboard.Aplication.DTOs
{
    public class BillboardDTO
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MovieID { get; set; }
        public required MovieDTO Movie { get; set; }
        public int RoomID { get; set; }
        public required RoomDTO Room { get; set; }
    }
}