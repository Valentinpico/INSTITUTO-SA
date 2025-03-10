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
    public class RoomController(IRoomService roomService) : ControllerBase
    {
        private readonly IRoomService _roomService = roomService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            var rooms = await _roomService.GetAllAsync();

            return Ok(
                ApiResponse<IEnumerable<RoomDTO>>.SuccessResponse(rooms, "Rooms listed")
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoomById(int id)
        {
            var room = await _roomService.GetByIdAsync(id);

            return Ok(
                ApiResponse<RoomDTO>.SuccessResponse(room, "Room found")
            );
        }

        [HttpPost]
        public async Task<ActionResult<RoomDTO>> CreateRoom(RoomCreatedDTO createRoomDTO)
        {
            await _roomService.AddAsync(createRoomDTO);

            return Ok(
                ApiResponse<RoomDTO>.SuccessResponse(null, "Room created")
            );


        }

        [HttpPut]
        public async Task<ActionResult<RoomDTO>> UpdateRoom(RoomDTO roomDTO)
        {
            await _roomService.UpdateAsync(roomDTO);

            return Ok(
                ApiResponse<RoomDTO>.SuccessResponse(null, "Room updated")
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteRoom(int id)
        {
            await _roomService.DeleteAsync(id);

            return Ok(
                ApiResponse<string>.SuccessResponse("Room deleted")
            );

        }
    }
}