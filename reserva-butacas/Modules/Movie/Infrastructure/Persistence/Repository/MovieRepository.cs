using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Modules.Movie.Domain.Entities;

namespace reserva_butacas.Modules.Movie.Infrastructure.Persistence.Repository
{
    public class MovieRepository : BaseRepository<MovieEntity>, IMovieRepository
    {

        public MovieRepository(AppDbContext context) : base(context)
        {

        }

    }
}