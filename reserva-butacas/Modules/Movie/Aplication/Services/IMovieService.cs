using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Modules.Movie.Aplication.DTOs;
using reserva_butacas.Modules.Movie.Domain.Entities;

namespace reserva_butacas.Modules.Movie.Aplication.Services
{
    public interface IMovieService : IBaseService<MovieEntity, MovieDTO, MovieCreatedDTO>
    {

    }
}