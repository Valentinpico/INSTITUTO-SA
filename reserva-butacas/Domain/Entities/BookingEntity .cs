using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Domain.Entities
{
    public class BookingEntity
    {
        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }

        [ForeignKey("Seat")]
        public int SeatId { get; set; }
        public SeatEntity Seat { get; set; }

        [ForeignKey("Billboard")]
        public int BillboardId { get; set; }
        public BillboardEntity Billboard { get; set; }
    }
}