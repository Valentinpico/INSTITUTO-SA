using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Infrastructure.Api;
using reserva_butacas.Modules.Billboard.Aplication.DTOs;
using reserva_butacas.Modules.Billboard.Aplication.Services;
using reserva_butacas.Modules.Billboard.Domain.Entities;

namespace reserva_butacas.Modules.Billboard.Infrastructure.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillboardController(IBillboardService billboardService, IMapper mapper) : ControllerBase
    {

        private readonly IBillboardService _billboardService = billboardService;
        private readonly IMapper _mapper = mapper;

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

        [HttpPost]
        public async Task<ActionResult<BillboardEntity>> Create(BillboardCreateDTO billboardCreateDTO)
        {
            var billboardCreate = _mapper.Map<BillboardEntity>(billboardCreateDTO);

            await _billboardService.AddAsync(billboardCreate);

            var billboardDTO = _mapper.Map<BillboardDTO>(billboardCreate);

            return Ok(ApiResponse<BillboardDTO>.SuccessResponse(billboardDTO, "Billboard creado"));
        }

        [HttpPut]
        public async Task<ActionResult<BillboardEntity>> Update(BillboardEntity billboard)
        {
            var billboardFind = await _billboardService.GetByIdAsync(billboard.Id)
            ?? throw new NotFoundException($"Cliente con ID {billboard.Id} no encontrado");

            await _billboardService.UpdateAsync(billboardFind);

            var billboardDTO = _mapper.Map<BillboardDTO>(billboardFind);

            return Ok(ApiResponse<BillboardDTO>.SuccessResponse(billboardDTO, "Billboard actualizado"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var billboard = await _billboardService.GetByIdAsync(id)
            ?? throw new NotFoundException($"Cliente con ID {id} no encontrado");

            await _billboardService.DeleteAsync(billboard.Id);

            return Ok(ApiResponse<string>.SuccessResponse("Billboard eliminado"));
        }

    }
}