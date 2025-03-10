using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using reserva_butacas.Aplication.Services;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Modules.Customer.Aplication.DTOs;
using reserva_butacas.Modules.Customer.Domain.Entities;
using reserva_butacas.Modules.Customer.Infrastructure.Persistence.Repository;

namespace reserva_butacas.Modules.Customer.Aplication.Services
{
    public class CustomerService(ICustomerRepository baseService, IMapper mapper) : ICustomerService
    {
        protected readonly ICustomerRepository _customerRepository = baseService;
        protected readonly IMapper _mapper = mapper;

        public async Task AddAsync(CustomerCreateDTO customerEntity)
        {
            var dcumentNumberExist = await _customerRepository.SearchAsync(x => x.DocumentNumber == customerEntity.DocumentNumber);

            if (dcumentNumberExist.Any())
            {
                throw new BadRequestException($"The Number {customerEntity.DocumentNumber} already exists in the database");
            }
            var emailExist = await _customerRepository.SearchAsync(x => x.Email == customerEntity.Email);

            if (emailExist.Any())
            {
                throw new BadRequestException($"The Email {customerEntity.Email} already exists in the database");
            }

            var customerADD = _mapper.Map<CustomerEntity>(customerEntity);


            await _customerRepository.AddAsync(customerADD);
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"The customer with id {id} does not exist");

            await _customerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            customers = customers.Where(c => c.Status);

            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);

        }

        public async Task<CustomerDTO> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"The customer with id {id} does not exist");

            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<IEnumerable<CustomerDTO>> SearchAsync(Func<CustomerEntity, bool> predicate)
        {
            var customers = await _customerRepository.SearchAsync(predicate);

            customers = customers.Where(c => c.Status);

            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }

        public async Task UpdateAsync(CustomerDTO customerEntity)
        {
            var customer = await _customerRepository.GetByIdAsync(customerEntity.Id)
                ?? throw new NotFoundException($"The customer with id {customerEntity.Id} does not exist");

            var dcumentNumberExist = await _customerRepository.SearchAsync(x => x.DocumentNumber == customerEntity.DocumentNumber && x.Id != customerEntity.Id);

            if (dcumentNumberExist.Any())
            {
                throw new BadRequestException($"The Number {customerEntity.DocumentNumber} already exists in the database");
            }

            var emailExist = await _customerRepository.SearchAsync(x => x.Email == customerEntity.Email && x.Id != customerEntity.Id);

            if (emailExist.Any())
            {
                throw new BadRequestException($"The Email {customerEntity.Email} already exists in the database");
            }

            var customerUpdated = _mapper.Map<CustomerEntity>(customerEntity);

            await _customerRepository.UpdateAsync(customerUpdated);
        }


    }
}