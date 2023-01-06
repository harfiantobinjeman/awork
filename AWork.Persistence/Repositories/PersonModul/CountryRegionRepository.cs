using AWork.Domain.Dto;
using AWork.Domain.Models;
using AWork.Domain.Repositories.PersonModul;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.PersonModul
{
    public class CountryRegionRepository : RepositoryBase<CountryRegion>, ICountryRegionRepository
    {
        public CountryRegionRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(CountryRegion countryRegion)
        {
            Update(countryRegion);
        }

        public async Task<IEnumerable<CountryRegion>> GetAllCountryRegion(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<CountryRegion> GetAllCountryRegionByCode(string countryCode, bool trackChanges)
        {
            return await FindByCondition(c => c.CountryRegionCode.Equals(countryCode), trackChanges)
                .Include(s => s.StateProvinces)
                .SingleOrDefaultAsync();
        }

        public void Insert(CountryRegion countryRegion)
        {
            Create(countryRegion);
        }

        public void Remove(CountryRegion countryRegion)
        {
            Delete(countryRegion);
        }
       /* public async Task<IEnumerable<TotalPersonByCountry>> GetTotalPersonByCountry(string countryRegionCode)
        {
            var rawSQL = await _dbContext.GetTotalPersonByCountrySQL
                .FromSqlRaw("select (c.CountryRegionCode)CountryRegionCode,count(pp.PersonType)TotalPerson from Person.CountryRegion c join Person.StateProvince s on c.CountryRegionCode=s.CountryRegionCode join Sales.SalesTerritory st on s.TerritoryID= st.TerritoryID join Sales.Customer sc on st.TerritoryID= sc.TerritoryID join Person.Person pp on sc.CustomerID=pp.BusinessEntityID group by c.CountryRegionCode")
                .Select(x => new TotalPersonByCountry
                {
                    CountryRegionCode = x.CountryRegionCode,
                    TotalPerson = x.TotalPerson

                })
                .OrderBy(x => x.CountryRegionCode)
                .ToListAsync();

            return rawSQL;*/
        
    }
}
