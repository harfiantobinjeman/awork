using AWork.Domain.Dto;
using AWork.Domain.Dto.Sales;
using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Sales
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomer(bool trackChanges);

        Task<Customer> GetCustomerById(int customerId, bool trackChanges);

        Task<IEnumerable<Person>> GetPersonCustomer(bool trackChanges);
        Task<IEnumerable<GetDataPersonCustomer>> GetData(int bussinessEntityId);

        Task<IEnumerable<GetDataTerritory>> GetDataTerritories(int territoryId);
        void Insert(Customer customer);

        void Edit(Customer customer);

        void Remove(Customer customer);
    }
}
