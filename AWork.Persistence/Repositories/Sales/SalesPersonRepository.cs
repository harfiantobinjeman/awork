using AWork.Domain.Base;
using AWork.Domain.Dto;
using AWork.Domain.Models;
using AWork.Domain.Repositories.Sales;
using AWork.Persistence.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.Sales
{
    public class SalesPersonRepository : RepositoryBase<SalesPerson>, ISalesPersonRepository
    {
        public SalesPersonRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(SalesPerson salesTeritory)
        {
            Update(salesTeritory);
        }

        public async Task<IEnumerable<SalesPerson>> GetAllSalesPerson(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(e => e.BusinessEntity)
                .Include(e => e.BusinessEntity.BusinessEntity)
                .Include(t => t.Territory)
                .OrderBy(b => b.BusinessEntityId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PersonEmployeeData>> GetDataEmployeeTypeSP()
        {
            var rawSQL = await _dbContext.PersonEmployeeDataSQL
                .FromSqlRaw("select p.BusinessEntityID, (p.FirstName + ' ' + p.LastName)FullName, e.JobTitle, e.NationalIDNumber " +
                "from Person.Person p " +
                "join HumanResources.Employee e " +
                "on p.BusinessEntityID = e.BusinessEntityID " +
                "where p.PersonType = 'SP' and not exists(select * from Sales.SalesPerson sp where sp.BusinessEntityID = p.BusinessEntityID) " +
                "group by p.BusinessEntityID, (p.FirstName + ' ' + p.LastName), e.JobTitle, e.NationalIDNumber")
                .Select(p => new PersonEmployeeData
                {
                    BusinessEntityId = p.BusinessEntityId,
                    FullName = p.FullName,
                    NationalIDNumber = p.NationalIDNumber,
                    JobTitle = p.JobTitle
                })
                .OrderBy(p => p.BusinessEntityId)
                .ToListAsync();
            return rawSQL;
        }

        public async Task<IEnumerable<GetDataTerritory>> GetDataTerritoryByName()
        {
            var rawSQL = await _dbContext.DataTerritorySQL
                .FromSqlRaw("select * from Sales.SalesTerritory")
                .Select(p => new GetDataTerritory
                {
                    TerritoryId = p.TerritoryId,
                    Name = p.Name,
                    CountryRegionCode = p.CountryRegionCode,
                    Group = p.Group
                })
                .OrderBy(p => p.Name)
                .ToListAsync();
            return rawSQL;
        }

        public async Task<SalesPerson> GetSalesPersonById(int businessEntityId, bool trackChanges)
        {
            return await FindByCondition(b => b.BusinessEntityId.Equals(businessEntityId), trackChanges)
                .Include(e => e.BusinessEntity)
                .Include(e => e.BusinessEntity.BusinessEntity)
                .Include(t => t.Territory)
                .Include(s => s.Stores)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> GetSalesPersonNotExistsEmployee(bool trackChanges)
        {
            return await _dbContext.People
                .Join(_dbContext.Employees, 
                p => p.BusinessEntityId, 
                e => e.BusinessEntityId,
                (p,e) => new {People = p, Employees = e})
                .Where(x => x.People.PersonType == "SP" && !_dbContext.SalesPeople.Any(sp => sp.BusinessEntityId == x.People.BusinessEntityId))
                .Select(x => x.People)
                .ToListAsync();
        }

        public void Insert(SalesPerson salesTeritory)
        {
            Create(salesTeritory);
        }

        public void Remove(SalesPerson salesTeritory)
        {
            Delete(salesTeritory);
        }
    }
}
