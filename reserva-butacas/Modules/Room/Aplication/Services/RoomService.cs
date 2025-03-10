using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Modules.Room.Aplication.DTOs;
using reserva_butacas.Modules.Room.Domain.Entities;
using reserva_butacas.Modules.Room.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Room.Aplication.Services
{
    public class RoomService(IRoomRepository roomRepository, IMapper mapper) : IRoomService
    {
        private readonly IRoomRepository _roomRepository = roomRepository;
        private readonly IMapper _mapper = mapper;

        public async Task AddAsync(RoomCreatedDTO entity)
        {
            var room = _mapper.Map<RoomEntity>(entity);

            await _roomRepository.AddAsync(room);
        }

        public async Task DeleteAsync(int id)
        {
            var roomExist = await _roomRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Room not found");

            await _roomRepository.DeleteAsync(id);

        }

        public async Task<IEnumerable<RoomDTO>> GetAllAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();

            rooms = rooms.Where(r => r.Status);

            return _mapper.Map<IEnumerable<RoomDTO>>(rooms);
        }

        public async Task<RoomDTO> GetByIdAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Room not found");

            return _mapper.Map<RoomDTO>(room);
        }

        public async Task<IEnumerable<RoomDTO>> SearchAsync(Func<RoomEntity, bool> predicate)
        {
            var rooms = await _roomRepository.SearchAsync(predicate);

            rooms = rooms.Where(r => r.Status);

            return _mapper.Map<IEnumerable<RoomDTO>>(rooms.Where(predicate));
        }

        public async Task UpdateAsync(RoomDTO entity)
        {
            var roomExist = await _roomRepository.GetByIdAsync(entity.Id)
                ?? throw new NotFoundException("Room not found");

            var room = _mapper.Map<RoomEntity>(entity);

            await _roomRepository.UpdateAsync(room);
        }
    }
}