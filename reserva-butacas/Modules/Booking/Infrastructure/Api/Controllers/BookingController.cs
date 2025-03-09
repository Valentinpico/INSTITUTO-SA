using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Infrastructure.Api;
using reserva_butacas.Modules.Booking.Aplication.DTOs;
using reserva_butacas.Modules.Booking.Aplication.Services;
using reserva_butacas.Modules.Booking.Domain.Entities;
using reserva_butacas.Modules.Booking.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Booking.Infrastructure.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController(
        IBookingService bookingService,
        IMapper mapper,
        IBookingRepository bookingRepository
    ) : ControllerBase
    {
        protected readonly IBookingService _bookingService = bookingService;
        private readonly IMapper _mapper = mapper;
        private readonly IBookingRepository _bookingRepository = bookingRepository;




        [HttpGet("horror")]
        public async Task<IActionResult> GetHorrorMovieBookings([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var bookings = await _bookingRepository.GetHorrorMovieBookingsInDateRange(startDate, endDate);

            var bookingsFound = bookings.Select(_mapper.Map<BookingDTO>);
            return Ok(
                ApiResponse<IEnumerable<BookingDTO>>.SuccessResponse(bookingsFound, "Bookings retrieved successfully")
            );
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _bookingService.GetAllAsync();

            var bookingsFound = result.Select(_mapper.Map<BookingDTO>);

            return Ok(
                ApiResponse<IEnumerable<BookingDTO>>.SuccessResponse(bookingsFound, "Bookings retrieved successfully")
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _bookingService.GetByIdAsync(id) ?? throw new NotFoundException($"Booking with ID {id} not found");

            var bookingFound = _mapper.Map<BookingDTO>(result);

            return Ok(
                ApiResponse<BookingDTO>.SuccessResponse(_mapper.Map<BookingDTO>(bookingFound), "Booking retrieved successfully")
            );
        }

        [HttpPost]
        public async Task<IActionResult> Post(BookingCreateDTO bookingCreateDTO)
        {
            var booking = _mapper.Map<BookingEntity>(bookingCreateDTO);
            await _bookingService.AddAsync(booking);
            var bookingCreated = _mapper.Map<BookingDTO>(booking);
            return Ok(
                  ApiResponse<BookingDTO>.SuccessResponse(bookingCreated, "Customer created successfully")
            );
        }

        [HttpPut]
        public async Task<IActionResult> Put(BookingDTO bookingDTO)
        {
            var booking = _mapper.Map<BookingEntity>(bookingDTO);

            await _bookingService.UpdateAsync(booking);

            var bookingUpdated = _mapper.Map<BookingDTO>(booking);

            return Ok(
                  ApiResponse<BookingDTO>.SuccessResponse(bookingUpdated, "Customer created successfully")
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookingService.DeleteAsync(id);
            return Ok(
                    ApiResponse<BookingDTO>.SuccessResponse(null, "Customer created successfully")
            );
        }




    }
}