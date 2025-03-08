using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Domain.Entities
{
    public class BookingEntity : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }

        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual required CustomerEntity Customer { get; set; }

        public int SeatID { get; set; }
        [ForeignKey("SeatID")]
        public virtual required SeatEntity Seat { get; set; }

        public int BillboardID { get; set; }
        [ForeignKey("BillboardID")]
        public virtual required BillboardEntity Billboard { get; set; }
    }
}