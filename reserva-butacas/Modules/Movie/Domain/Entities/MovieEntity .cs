using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Modules.Movie.Domain.Enums;

namespace reserva_butacas.Modules.Movie.Domain.Entities
{
    public class MovieEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [EnumDataType(typeof(MovieGenreEnum))]
        public MovieGenreEnum Genre { get; set; }

        [Required]
        public short AllowedAge { get; set; }

        [Required]
        public short LengthMinutes { get; set; }
    }
}