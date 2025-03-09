using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Infrastructure.Api;
using reserva_butacas.Modules.Movie.Aplication.DTOs;
using reserva_butacas.Modules.Movie.Aplication.Services;
using reserva_butacas.Modules.Movie.Domain.Entities;

namespace reserva_butacas.Modules.Movie.Infrastructure.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController(IMapper mapper, IMovieService movieService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMovieService _movieService = movieService;


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            var movies = await _movieService.GetAllAsync();

            var moviesDTO = _mapper.Map<List<MovieDTO>>(movies);

            return Ok(
                ApiResponse<IEnumerable<MovieDTO>>.SuccessResponse(moviesDTO)
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovieById(int id)
        {
            var movie = await _movieService.GetByIdAsync(id)
                        ?? throw new NotFoundException($"Movie with ID {id} not found");

            var movieDTO = _mapper.Map<MovieDTO>(movie);

            return Ok(
                ApiResponse<MovieDTO>.SuccessResponse(movieDTO)
            );
        }

        [HttpPost]
        public async Task<ActionResult<MovieDTO>> CreateMovie(MovieCreatedDTO createMovieDTO)
        {


            var movieEntity = _mapper.Map<MovieEntity>(createMovieDTO);

            await _movieService.AddAsync(movieEntity);

            var movieCreated = _mapper.Map<MovieDTO>(movieEntity);

            return Ok(
                ApiResponse<MovieDTO>.SuccessResponse(movieCreated)
            );
        }

        [HttpPut]
        public async Task<ActionResult<MovieDTO>> UpdateMovie(MovieDTO movieDTO)
        {
            var movie = await _movieService.GetByIdAsync(movieDTO.Id)
                        ?? throw new NotFoundException($"Movie with ID {movieDTO.Id} not found");

            _mapper.Map(movieDTO, movie);

            await _movieService.UpdateAsync(movie);

            return Ok(
                ApiResponse<MovieDTO>.SuccessResponse(movieDTO)
            );
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieDTO>> DeleteMovie(int id)
        {
            var movie = await _movieService.GetByIdAsync(id)
                        ?? throw new NotFoundException($"Movie with ID {id} not found");

            await _movieService.DeleteAsync(id);

            return Ok(
                ApiResponse<MovieDTO>.SuccessResponse(null)
            );

        }
    }
}