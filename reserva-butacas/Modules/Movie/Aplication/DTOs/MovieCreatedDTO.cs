using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Modules.Movie.Domain.Enums;

namespace reserva_butacas.Modules.Movie.Aplication.DTOs
{
    public class MovieCreatedDTO
    {

        public required string Name { get; set; }

        public MovieGenreEnum Genre { get; set; }

        public short AllowedAge { get; set; }

        public short LengthMinutes { get; set; }
    }
}