using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Modules.Seat.Aplication.DTOs;
using reserva_butacas.Modules.Seat.Domain.Entities;

namespace reserva_butacas.Modules.Room.Domain.Entities
{
    public class RoomEntity : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        public short Number { get; set; }

        public required ICollection<SeatEntity> Seats { get; set; }


    }
}