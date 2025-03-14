using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Domain.Entities;
using AutoMapper;
using reserva_butacas.Modules.Billboard.Aplication.DTOs;
using reserva_butacas.Modules.Billboard.Domain.Entities;
using reserva_butacas.Modules.Customer.Domain.Entities;
using reserva_butacas.Modules.Customer.Aplication.DTOs;
using reserva_butacas.Modules.Booking.Aplication.DTOs;
using reserva_butacas.Modules.Booking.Domain.Entities;
using reserva_butacas.Modules.Movie.Domain.Entities;
using reserva_butacas.Modules.Movie.Aplication.DTOs;
using reserva_butacas.Modules.Room.Domain.Entities;
using reserva_butacas.Modules.Room.Aplication.DTOs;
using reserva_butacas.Modules.Seat.Domain.Entities;
using reserva_butacas.Modules.Seat.Aplication.DTOs;
namespace reserva_butacas.Domain.AutoMappers
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            //Customer
            CreateMap<CustomerEntity, CustomerDTO>();
            CreateMap<CustomerDTO, CustomerEntity>();
            CreateMap<CustomerUpdateDTO, CustomerEntity>();
            CreateMap<CustomerCreateDTO, CustomerEntity>();

            //Billboard
            CreateMap<BillboardEntity, BillboardDTO>();
            CreateMap<BillboardDTO, BillboardEntity>();
            CreateMap<BillboardCreateDTO, BillboardEntity>();
            CreateMap<BillboardUpdateDTO, BillboardEntity>();

            //Booking
            CreateMap<BookingEntity, BookingDTO>();
            CreateMap<BookingDTO, BookingEntity>();
            CreateMap<BookingCreateDTO, BookingEntity>();
            CreateMap<BookingUpdateDTO, BookingEntity>();

            //Movie
            CreateMap<MovieEntity, MovieDTO>();
            CreateMap<MovieDTO, MovieEntity>();
            CreateMap<MovieCreatedDTO, MovieEntity>();

            //Room
            CreateMap<RoomEntity, RoomDTO>();
            CreateMap<RoomDTO, RoomEntity>();
            CreateMap<RoomCreatedDTO, RoomEntity>();

            //seat 
            CreateMap<SeatEntity, SeatDTO>();
            CreateMap<SeatDTO, SeatEntity>();
            CreateMap<SeatCreateDTO, SeatEntity>();

        }

    }
}