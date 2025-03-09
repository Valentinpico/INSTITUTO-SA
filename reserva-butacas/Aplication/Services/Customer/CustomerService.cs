using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Domain.Exeptions;
using reserva_butacas.Infrastructure.Persistence.Repositories.Billboard;

namespace reserva_butacas.Aplication.Services.Customer
{
    public class CustomerService(ICustomerRepository baseService) : BaseService<CustomerEntity>(baseService), ICustomerService
    {
        protected readonly ICustomerRepository _customerRepository = baseService;

        public new async Task AddAsync(CustomerEntity customerEntity)
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


            await _customerRepository.AddAsync(customerEntity);
        }


        public new async Task UpdateAsync(CustomerEntity customerEntity)
        {
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

            await _customerRepository.UpdateAsync(customerEntity);
        }
    }
}