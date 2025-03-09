using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Infrastructure.Api;
using reserva_butacas.Modules.Billboard.Aplication.Services;
using reserva_butacas.Modules.Billboard.Domain.Entities;

namespace reserva_butacas.Modules.Billboard.Infrastructure.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillboardController(IBillboardService billboardService) : ControllerBase
    {

        private readonly IBillboardService _billboardService = billboardService;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<BillboardEntity>>>> GetAll()
        {
            var billboards = await _billboardService.GetAllAsync();

            return Ok(
                ApiResponse<IEnumerable<BillboardEntity>>.SuccessResponse(billboards, "Billboards lista")
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BillboardEntity>> GetById(int id)
        {
            var billboard = await _billboardService.GetByIdAsync(id)
            ?? throw new NotFoundException($"Cliente con ID {id} no encontrado");

            return Ok(ApiResponse<BillboardEntity>.SuccessResponse(billboard, "Billboard encontrado"));
        }

    }
}