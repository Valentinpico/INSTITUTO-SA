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

            return Ok(ApiResponse<IEnumerable<MovieDTO>>.SuccessResponse(movies));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovieById(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            return Ok(
                ApiResponse<MovieDTO>.SuccessResponse(movie)
            );
        }

        [HttpPost]
        public async Task<ActionResult<MovieDTO>> CreateMovie(MovieCreatedDTO createMovieDTO)
        {
            await _movieService.AddAsync(createMovieDTO);

            return Ok(
                ApiResponse<MovieDTO>.SuccessResponse(null, "Movie created successfully")
            );
        }

        [HttpPut]
        public async Task<ActionResult<MovieDTO>> UpdateMovie(MovieDTO movieDTO)
        {
            await _movieService.UpdateAsync(movieDTO);

            return Ok(
                ApiResponse<MovieDTO>.SuccessResponse(movieDTO, "Movie updated successfully")
            );
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieDTO>> DeleteMovie(int id)
        {
            await _movieService.DeleteAsync(id);

            return Ok(
                ApiResponse<MovieDTO>.SuccessResponse(null, "Movie deleted successfully")
            );

        }
    }
}