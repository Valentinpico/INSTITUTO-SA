using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Modules.Room.Domain.Entities;

namespace reserva_butacas.Modules.Seat.Domain.Entities
{
    public class SeatEntity : BaseEntity
    {
        [Required]
        public short Number { get; set; }

        [Required]
        public short RowNumber { get; set; }

        public int RoomID { get; set; }
        
        [ForeignKey("RoomID")]
        public virtual required RoomEntity Room { get; set; }
    }
}