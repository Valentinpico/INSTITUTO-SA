using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Modules.Movie.Aplication.DTOs;
using reserva_butacas.Modules.Movie.Domain.Entities;
using reserva_butacas.Modules.Movie.Domain.Enums;
using reserva_butacas.Modules.Movie.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Movie.Aplication.Services
{
    public class MovieService(IMovieRepository movieRepository, IMapper mapper) : BaseService<MovieEntity>(movieRepository), IMovieService
    {
        private readonly IMovieRepository _movieRepository = movieRepository;
        private readonly IMapper _mapper = mapper;


        public new async Task AddAsync(MovieEntity movieCreatedDTO)
        {

            if (!Enum.IsDefined(typeof(MovieGenreEnum), movieCreatedDTO.Genre))
            {
                throw new BadRequestException("Genre not valid");
            }

            await _movieRepository.AddAsync(movieCreatedDTO);
        }


    }
}