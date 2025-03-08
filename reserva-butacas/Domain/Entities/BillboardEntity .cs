using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Domain.Entities
{
    public class BillboardEntity
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public MovieEntity Movie { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public RoomEntity Room { get; set; }
    }
}