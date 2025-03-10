using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Infrastructure.Api;
using reserva_butacas.Modules.Customer.Aplication.DTOs;
using reserva_butacas.Modules.Customer.Aplication.Services;
using reserva_butacas.Modules.Customer.Domain.Entities;

namespace reserva_butacas.Modules.Customer.Infrastructure.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(ICustomerService customerService, IMapper mapper) : ControllerBase
    {

        private readonly ICustomerService _customerService = customerService;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAll()
        {
            var customers = await _customerService.GetAllAsync() ?? [];

            return Ok(
                ApiResponse<IEnumerable<CustomerDTO>>.SuccessResponse(customers, "Customers retrieved successfully")
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            return Ok(
                ApiResponse<CustomerDTO>.SuccessResponse(customer, "Customer retrieved successfully")
            );
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> Create(CustomerCreateDTO createCustomerDTO)
        {
            await _customerService.AddAsync(createCustomerDTO);

            return Ok(
                ApiResponse<CustomerDTO>.SuccessResponse(null, "Customer created successfully")
            );
        }

        [HttpPut]
        public async Task<ActionResult<CustomerDTO>> Update(CustomerDTO updateCustomerDTO)
        {
            await _customerService.UpdateAsync(updateCustomerDTO);

            return Ok(
                ApiResponse<CustomerDTO>.SuccessResponse(null, "Customer updated successfully")
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDTO>> Delete(int id)
        {
            await _customerService.DeleteAsync(id);

            return Ok(
                ApiResponse<CustomerDTO>.SuccessResponse(null, "Customer deleted successfully")
            );

        }
    }

}