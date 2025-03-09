using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Domain.Entities;

namespace reserva_butacas.Modules.Customer.Domain.Entities

{
    public class CustomerEntity : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public required string DocumentNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public required string Lastname { get; set; }

        [Required]
        public short Age { get; set; }

        [MaxLength(20)]
        public required string PhoneNumber { get; set; }

        [MaxLength(100)]
        public required string Email { get; set; }
    }
}