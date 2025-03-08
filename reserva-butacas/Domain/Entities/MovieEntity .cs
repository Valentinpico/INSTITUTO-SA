using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Domain.Enums;

namespace reserva_butacas.Domain.Entities
{
    public class MovieEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        public MovieGenreEnum Genre { get; set; }

        [Required]
        public short AllowedAge { get; set; }

        [Required]
        public short LengthMinutes { get; set; }
    }
}