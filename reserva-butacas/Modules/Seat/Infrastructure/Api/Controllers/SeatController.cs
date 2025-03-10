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
    public class SeatController(IMapper mapper, ISeatService seatService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ISeatService _seatService = seatService;

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelSeatAndBooking(SeatCancellationDTO dto)
        {
            await _seatService.CancelSeatAndBookingAsync(dto);
            return Ok(
                ApiResponse<object>.SuccessResponse(null, "Seat and booking canceled successfully")
            );
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeatDTO>>> Get()
        {
            var seats = await _seatService.GetAllAsync();

            var seatsDTO = _mapper.Map<IEnumerable<SeatDTO>>(seats);
            return Ok(
                ApiResponse<IEnumerable<SeatDTO>>.SuccessResponse(seatsDTO, "Seats listed successfully")
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var seat = await _seatService.GetByIdAsync(id)
            ?? throw new NotFoundException("Seat not found");

            var seatDTO = _mapper.Map<SeatDTO>(seat);
            return Ok(
                ApiResponse<SeatDTO>.SuccessResponse(seatDTO, "Seat listed successfully")
            );

        }

        [HttpPost]
        public async Task<IActionResult> Post(SeatCreateDTO seatCreateDTO)
        {
            var seat = _mapper.Map<SeatEntity>(seatCreateDTO);

            await _seatService.AddAsync(seat);

            var seatDTO = _mapper.Map<SeatDTO>(seat);

            return Ok(
                ApiResponse<SeatDTO>.SuccessResponse(seatDTO, "Seat created successfully")
            );
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<SeatDTO>>> Put(SeatDTO seatEntity)
        {
            var seat = _mapper.Map<SeatEntity>(seatEntity);

            await _seatService.UpdateAsync(seat);

            var seatDTO = _mapper.Map<SeatDTO>(seat);

            return Ok(
                ApiResponse<SeatDTO>.SuccessResponse(seatDTO, "Seat updated successfully")
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