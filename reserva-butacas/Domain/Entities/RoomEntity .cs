using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Domain.Entities
{
    public class RoomEntity
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        public short Number { get; set; }

    }
}