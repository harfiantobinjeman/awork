using AutoMapper;
using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.SalesCustomer;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Sales
{
    public class CustomerService : ICustomerService
    {
        private readonly ISalesRepositoryManager _salesRepositoryManager;
        private readonly IMapper _mapper;

        public CustomerService(ISalesRepositoryManager salesRepositoryManager, IMapper mapper)
        {
            _salesRepositoryManager = salesRepositoryManager;
            _mapper = mapper;
        }

        public void Change(CustomerDto customerDto)
        {
            var change = _mapper.Map<Customer>(customerDto);
            _salesRepositoryManager.CustomerRepository.Edit(change);
            _salesRepositoryManager.Save();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomer(bool trackChanges)
        {
            var customerModel = await _salesRepositoryManager.CustomerRepository.GetAllCustomer(trackChanges);
            var customerDto = _mapper.Map<IEnumerable<CustomerDto>>(customerModel);
            return customerDto;
        }

        public async Task<CustomerDto> GetCustomerById(int customerId, bool trackChanges)
        {
            var customerModel = await _salesRepositoryManager.CustomerRepository.GetCustomerById(customerId, trackChanges);
            var customerDto = _mapper.Map<CustomerDto>(customerModel);
            return customerDto;
        }

        public async Task<IEnumerable<PersonDto>> GetPersonCustomer(bool trackChanges)
        {
            var personCustomerModel = await _salesRepositoryManager.CustomerRepository.GetPersonCustomer(trackChanges);
            var personCustomerDto = _mapper.Map<IEnumerable<PersonDto>>(personCustomerModel);
            return personCustomerDto;
        }

        public void Insert(CustomerForCreateDto customerForCreateDto)
        {
            var baru =_mapper.Map<Customer>(customerForCreateDto);
            _salesRepositoryManager.CustomerRepository.Insert(baru);
            _salesRepositoryManager.Save();
        }

        public void Remove(CustomerDto customerDto)
        {
            var hapus = _mapper.Map<Customer>(customerDto);
            _salesRepositoryManager.CustomerRepository.Remove(hapus);
            _salesRepositoryManager.Save();
        }
    }
}
