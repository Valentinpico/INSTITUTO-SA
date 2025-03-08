using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Domain.Entities
{
    public class BillboardEntity : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }

        public int MovieID { get; set; }
        [ForeignKey("MovieID")]
        public virtual required MovieEntity Movie { get; set; }

        public int RoomID { get; set; }
        [ForeignKey("RoomID")]
        public virtual required RoomEntity Room { get; set; }
    }
}