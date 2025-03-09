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