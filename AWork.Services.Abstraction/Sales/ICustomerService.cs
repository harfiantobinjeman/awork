using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.SalesCustomer;
using AWork.Contracts.Dto.Sales.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Sales
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomer (bool trackChanges);
        Task<CustomerDto> GetCustomerById(int customerId, bool trackChanges);
        Task<IEnumerable<PersonDto>> GetPersonCustomer(bool trackChanges);
        void Insert(CustomerForCreateDto customerForCreateDto);
        void Remove(CustomerDto customerDto);
        void Change(CustomerDto customerDto);

    }
}
