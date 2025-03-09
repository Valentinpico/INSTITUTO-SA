using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Aplication.DTOs.Customer;
using reserva_butacas.Domain.Entities;
using AutoMapper;
using reserva_butacas.Aplication.DTOs.Billboard;
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





        }

    }
}