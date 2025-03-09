using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Aplication.DTOs.Customer
{
    public class CustomerUpdateDTO
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public required string DocumentNumber { get; set; }
        public required string Name { get; set; }
        public required string Lastname { get; set; }
        public short Age { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
    }
}