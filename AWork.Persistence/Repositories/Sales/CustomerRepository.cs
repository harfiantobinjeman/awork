using AWork.Domain.Dto;
using AWork.Domain.Dto.Sales;
using AWork.Domain.Models;
using AWork.Domain.Repositories.Sales;
using AWork.Persistence.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Sales
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(Customer customer)
        {
            Update(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomer(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.CustomerId)
                .Include(e => e.Person)
                .Include(f=>f.Store)
                .Include(g=>g.Territory)
                .Where(c=>c.PersonId !=null && c.Person.PersonType=="IN")
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int customerId, bool trackChanges)
        {
            return await FindByCondition(c => c.CustomerId.Equals(customerId), trackChanges)
                .Include(e => e.Person)
                .Include(f => f.Store)
                .Include(g => g.Territory)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> GetPersonCustomer(bool trackChanges)
        {
            var personCustomer = await _dbContext.People
                .Where(p => p.PersonType == "IN" && !_dbContext.Customers.Any(e => e.PersonId == p.BusinessEntityId))
                .ToListAsync();
            return personCustomer;
        }

        public async Task<IEnumerable<GetDataPersonCustomer>> GetData(int bussinessEntityId)
        {
            var businessEntity = new SqlParameter("@paramId", bussinessEntityId);
            var rawSQL = await _dbContext.DataPersonCustomerSQL
                .FromSqlRaw("select p.BusinessEntityID, (p.FirstName+' '+p.LastName)FullName, p.Suffix " +
                "from Person.Person p " +
                "where p.BusinessEntityID = @paramId " +
                "group by p.BusinessEntityID,(p.FirstName+' '+p.LastName),p.Suffix", businessEntity)
                .Select(p => new GetDataPersonCustomer
                {
                    BusinessEntityId = p.BusinessEntityId,
                    FullName = p.FullName,
                    Suffix = p.Suffix
                })
                .OrderBy(p => p.BusinessEntityId)
                .ToListAsync();
            return rawSQL;
        }

        public async Task<IEnumerable<GetDataTerritory>> GetDataTerritories(int territoryId)
        {
            var territory = new SqlParameter("@Id", territoryId);
            var rawSQL = await _dbContext.DataTerritorySQL
                .FromSqlRaw("select * from Sales.SalesTerritory where TerritoryID=@Id", territory)
                .Select(t => new GetDataTerritory
                {
                    Name = t.Name,
                    CountryRegionCode = t.CountryRegionCode,
                    Group = t.Group
                })
                .ToListAsync();
            return rawSQL;
        }

        public void Insert(Customer customer)
        {
            Create(customer);
        }

        public void Remove(Customer customer)
        {
            Delete(customer);
        }
    }
}
