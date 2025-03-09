using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using reserva_butacas.Aplication.Services.Billboard;
using reserva_butacas.Domain.Entities;

namespace reserva_butacas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillboardController(IBillboardService billboardService) : ControllerBase
    {

        private readonly IBillboardService _billboardService = billboardService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillboardEntity>>> GetAll()
        {
            var billboards = await _billboardService.GetAllAsync();

            if (billboards == null)
            {
                return NotFound();
            }

            return Ok(billboards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BillboardEntity>> GetById(int id)
        {
            var billboard = await _billboardService.GetByIdAsync(id);

            if (billboard == null)
            {
                return NotFound(new { message = "Billboard not found" });
            }

            return Ok(billboard);
        }

    }
}