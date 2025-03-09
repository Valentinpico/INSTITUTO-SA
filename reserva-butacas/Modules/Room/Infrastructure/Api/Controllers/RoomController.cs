using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Infrastructure.Api;
using reserva_butacas.Modules.Room.Aplication.DTOs;
using reserva_butacas.Modules.Room.Aplication.Services;
using reserva_butacas.Modules.Room.Domain.Entities;

namespace reserva_butacas.Modules.Room.Infrastructure.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController(IMapper mapper, IRoomService roomService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IRoomService _roomService = roomService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            var rooms = await _roomService.GetAllAsync();

            var roomsDTO = _mapper.Map<List<RoomDTO>>(rooms);

            return Ok(
                ApiResponse<IEnumerable<RoomDTO>>.SuccessResponse(roomsDTO)
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoomById(int id)
        {
            var room = await _roomService.GetByIdAsync(id)
                        ?? throw new NotFoundException($"Room with ID {id} not found");

            var roomDTO = _mapper.Map<RoomDTO>(room);

            return Ok(
                ApiResponse<RoomDTO>.SuccessResponse(roomDTO)
            );
        }

        [HttpPost]
        public async Task<ActionResult<RoomDTO>> CreateRoom(RoomCreatedDTO createRoomDTO)
        {
            var roomEntity = _mapper.Map<RoomEntity>(createRoomDTO);

            await _roomService.AddAsync(roomEntity);

            var roomCreated = _mapper.Map<RoomDTO>(roomEntity);

            return Ok(
                ApiResponse<RoomDTO>.SuccessResponse(roomCreated)
            );


        }

        [HttpPut]
        public async Task<ActionResult<RoomDTO>> UpdateRoom(RoomDTO roomDTO)
        {
            var roomEntity = _mapper.Map<RoomEntity>(roomDTO);

            await _roomService.UpdateAsync(roomEntity);

            var roomUpdated = _mapper.Map<RoomDTO>(roomEntity);

            return Ok(
                ApiResponse<RoomDTO>.SuccessResponse(roomUpdated)
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteRoom(int id)
        {
            var room = await _roomService.GetByIdAsync(id)
                        ?? throw new NotFoundException($"Room with ID {id} not found");

            await _roomService.DeleteAsync(room.Id);

            return Ok(
                ApiResponse<string>.SuccessResponse("Room deleted")
            );

        }
    }
}