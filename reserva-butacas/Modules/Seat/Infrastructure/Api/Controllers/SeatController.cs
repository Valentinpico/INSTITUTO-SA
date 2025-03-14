using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Infrastructure.Api;
using reserva_butacas.Modules.Seat.Aplication.DTOs;
using reserva_butacas.Modules.Seat.Aplication.Services;
using reserva_butacas.Modules.Seat.Domain.Entities;

namespace reserva_butacas.Modules.Seat.Infrastructure.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatController(ISeatService seatService) : ControllerBase
    {
        private readonly ISeatService _seatService = seatService;

        [HttpPost("cancel/{idSeat}")]
        public async Task<IActionResult> CancelSeatAndBooking(int idSeat)
        {

            Console.WriteLine("CancelSeatAndBooking: ====================================" + idSeat);
            await _seatService.CancelSeatAndBookingAsync(idSeat);
            return Ok(
                ApiResponse<object>.SuccessResponse(null, "Seat and booking canceled successfully")
            );
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeatDTO>>> Get()
        {
            var seats = await _seatService.GetAllAsync();

            return Ok(
                ApiResponse<IEnumerable<SeatDTO>>.SuccessResponse(seats, "Seats listed successfully")
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var seat = await _seatService.GetByIdAsync(id);

            return Ok(
                ApiResponse<SeatDTO>.SuccessResponse(seat, "Seat listed successfully")
            );

        }

        [HttpPost]
        public async Task<IActionResult> Post(SeatCreateDTO seatCreateDTO)
        {
            await _seatService.AddAsync(seatCreateDTO);

            return Ok(
                ApiResponse<SeatDTO>.SuccessResponse(null, "Seat created successfully")
            );
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<SeatDTO>>> Put(SeatDTO seatEntity)
        {
            await _seatService.UpdateAsync(seatEntity);

            return Ok(
                ApiResponse<SeatDTO>.SuccessResponse(null, "Seat updated successfully")
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _seatService.DeleteAsync(id);
            return Ok(
                ApiResponse<object>.SuccessResponse(null, "Seat deleted successfully")
            );
        }

    }
}