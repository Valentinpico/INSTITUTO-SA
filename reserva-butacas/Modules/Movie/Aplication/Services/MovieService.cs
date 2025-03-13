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
    public class MovieService(IMovieRepository movieRepository, IMapper mapper) : IMovieService
    {
        private readonly IMovieRepository _movieRepository = movieRepository;
        private readonly IMapper _mapper = mapper;


        public async Task AddAsync(MovieCreatedDTO movieCreatedDTO)
        {

            if (!Enum.IsDefined(typeof(MovieGenreEnum), movieCreatedDTO.Genre))
            {
                throw new BadRequestException("Genre not valid");
            }

            if (movieCreatedDTO.LengthMinutes <= 0)
            {
                throw new BadRequestException("Duration not valid ");
            }

            var nameExist = await _movieRepository.SearchAsync(x => x.Name == movieCreatedDTO.Name);

            if (nameExist.Any())
            {
                throw new BadRequestException($"The name {movieCreatedDTO.Name} already exists in the database");
            }

            var movieEntity = _mapper.Map<MovieEntity>(movieCreatedDTO);

            await _movieRepository.AddAsync(movieEntity);
        }



        public async Task DeleteAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Movie not found");

            await _movieRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MovieDTO>> GetAllAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            movies = movies.Where(x => x.Status);

            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public async Task<MovieDTO> GetByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Movie not found");

            return _mapper.Map<MovieDTO>(movie);
        }

        public async Task<IEnumerable<MovieDTO>> SearchAsync(Func<MovieEntity, bool> predicate)
        {
            var movies = await _movieRepository.GetAllAsync();
            movies = movies.Where(x => x.Status);

            return _mapper.Map<IEnumerable<MovieDTO>>(movies.Where(predicate));
        }

        public async Task UpdateAsync(MovieDTO entity)
        {
            var movie = await _movieRepository.GetByIdAsync(entity.Id)
                ?? throw new NotFoundException("Movie not found");

            if (!Enum.IsDefined(typeof(MovieGenreEnum), entity.Genre))
            {
                throw new BadRequestException("Genre not valid");
            }

            if (entity.LengthMinutes <= 0)
            {
                throw new BadRequestException("Duration not valid ");
            }

            movie = _mapper.Map<MovieEntity>(entity);

            await _movieRepository.UpdateAsync(movie);
        }
    }
}