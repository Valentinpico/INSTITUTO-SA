using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public int RoomID { get; set; }
    }
}