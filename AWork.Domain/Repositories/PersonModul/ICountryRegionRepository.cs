using AWork.Domain.Dto;
using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.PersonModul
{
    public interface ICountryRegionRepository
    {
        Task<IEnumerable<CountryRegion>> GetAllCountryRegion(bool trackChanges);
        Task<CountryRegion> GetAllCountryRegionByCode(string countryCode, bool trackChanges);
     /*   Task<IEnumerable<TotalPersonByCountry>> GetTotalPersonByCountry(string countryRegionCode);*/
        void Insert(CountryRegion countryRegion);
        void Edit(CountryRegion countryRegion);
        void Remove(CountryRegion countryRegion);
    }
}
