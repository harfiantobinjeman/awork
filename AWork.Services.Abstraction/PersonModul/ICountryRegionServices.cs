using AWork.Contracts.Dto.PersonModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface ICountryRegionServices
    {
        Task<IEnumerable<CountryRegionDto>> GetAllCountryRegion(bool trackChanges);
        Task<CountryRegionDto> GetAllCountryRegionByCode(string countryCode, bool trackChanges);
        CountryRegionDto CountryRegion();
        void Insert(CountryRegionForCreateDto countryRegionForCreateDto);
        void Edit(CountryRegionDto countryRegionDto);
        void Remove(CountryRegionDto countryRegionDto);
    }
}
