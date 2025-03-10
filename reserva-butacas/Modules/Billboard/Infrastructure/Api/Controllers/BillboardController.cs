using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Infrastructure.Api;
using reserva_butacas.Modules.Billboard.Aplication.DTOs;
using reserva_butacas.Modules.Billboard.Aplication.Services;
using reserva_butacas.Modules.Seat.Aplication.DTOs;
using reserva_butacas.Modules.Seat.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Billboard.Infrastructure.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillboardController(
        IBillboardService billboardService,
        ISeatRepository seatRepository) : ControllerBase
    {

        private readonly IBillboardService _billboardService = billboardService;
        private readonly ISeatRepository _seatRepository = seatRepository;


        [HttpPost("cancel-seat")]
        public async Task<IActionResult> CancelSeatAndBooking(SeatCancellationDTO dto)
        {
            await _billboardService.CancelSeatAndBookingAsync(dto);
            return Ok(
                ApiResponse<object>.SuccessResponse(null, "Seat and booking canceled successfully")
            );
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelBillboard(BillboardCancellationDTO dto)
        {
            var affectedCustomers = await _billboardService.CancelBillboardAndBookingsAsync(dto);
            return Ok(new
            {
                Message = "Billboard and associated bookings cancelled successfully",
                AffectedCustomers = affectedCustomers.Select(c => new
                {
                    c.Name,
                    c.Lastname,
                    c.DocumentNumber
                })
            });
        }

        [HttpGet("seats-availability/today")]
        public async Task<IActionResult> GetSeatsAvailabilityForToday()
        {
            var result = await _seatRepository.GetSeatAvailabilityByRoomForToday();
            return Ok(
                ApiResponse<object>.SuccessResponse(result, "Seats availability for today")
            );
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<BillboardDTO>>>> GetAll()
        {
            var billboards = await _billboardService.GetAllAsync();

            return Ok(
                ApiResponse<IEnumerable<BillboardDTO>>.SuccessResponse(billboards, "Billboards lista")
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BillboardDTO>> GetById(int id)
        {
            var billboard = await _billboardService.GetByIdAsync(id)
            ?? throw new NotFoundException($"Cliente con ID {id} no encontrado");

            return Ok(ApiResponse<BillboardDTO>.SuccessResponse(billboard, "Billboard encontrado"));
        }

        [HttpPost]
        public async Task<ActionResult<BillboardDTO>> Create(BillboardCreateDTO billboardCreateDTO)
        {


            await _billboardService.AddAsync(billboardCreateDTO);


            return Ok(ApiResponse<BillboardDTO>.SuccessResponse(null, "Billboard creado"));
        }

        [HttpPut]
        public async Task<ActionResult<BillboardDTO>> Update(BillboardDTO billboard)
        {
            await _billboardService.UpdateAsync(billboard);

            return Ok(ApiResponse<BillboardDTO>.SuccessResponse(billboard, "Billboard actualizado"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            await _billboardService.DeleteAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse("Billboard eliminado"));
        }

    }
}