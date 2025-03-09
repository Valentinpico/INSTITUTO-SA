using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reserva_butacas.Aplication.DTOs.Customer;
using reserva_butacas.Aplication.Services.Customer;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Domain.Exeptions;

namespace reserva_butacas.Infrastructure.Api.Controllers
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

            var customersFound = customers.Select(_mapper.Map<CustomerDTO>);

            return Ok(
                ApiResponse<IEnumerable<CustomerDTO>>.SuccessResponse(customersFound, "Customers retrieved successfully")
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id)
                        ?? throw new NotFoundException($"Customer with ID {id} not found");

            var customerFound = _mapper.Map<CustomerDTO>(customer);

            return Ok(
                ApiResponse<CustomerDTO>.SuccessResponse(customerFound, "Customer retrieved successfully")
            );
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> Create(CustomerCreateDTO createCustomerDTO)
        {
            var customer = _mapper.Map<CustomerEntity>(createCustomerDTO);

            await _customerService.AddAsync(customer);

            var customerCreated = _mapper.Map<CustomerDTO>(customer);

            return Ok(
                ApiResponse<CustomerDTO>.SuccessResponse(customerCreated, "Customer created successfully")
            );
        }

        [HttpPut]
        public async Task<ActionResult<CustomerDTO>> Update(CustomerUpdateDTO updateCustomerDTO)
        {
            var customer = await _customerService.GetByIdAsync(updateCustomerDTO.Id)
                        ?? throw new NotFoundException($"Customer with ID {updateCustomerDTO.Id} not found");

            _mapper.Map(updateCustomerDTO, customer);

            await _customerService.UpdateAsync(customer);

            var customerUpdated = _mapper.Map<CustomerDTO>(customer);

            return Ok(
                ApiResponse<CustomerDTO>.SuccessResponse(customerUpdated, "Customer updated successfully")
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDTO>> Delete(int id)
        {
            var customer = await _customerService.GetByIdAsync(id)
                        ?? throw new NotFoundException($"Customer with ID {id} not found");

            await _customerService.DeleteAsync(customer.Id);

            var customerDeleted = _mapper.Map<CustomerDTO>(customer);

            return Ok(
                ApiResponse<CustomerDTO>.SuccessResponse(customerDeleted, "Customer deleted successfully")
            );

        }
    }

}