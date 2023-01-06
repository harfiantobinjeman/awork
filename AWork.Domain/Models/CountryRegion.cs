using System;
using System.Collections.Generic;

#nullable disable

namespace AWork.Domain.Models
{
    public partial class CountryRegion
    {
        public CountryRegion()
        {
            CountryRegionCurrencies = new HashSet<CountryRegionCurrencyDto>();
            SalesTerritories = new HashSet<SalesTerritory>();
            StateProvinces = new HashSet<StateProvince>();
        }

        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<CountryRegionCurrencyDto> CountryRegionCurrencies { get; set; }
        public virtual ICollection<SalesTerritory> SalesTerritories { get; set; }
        public virtual ICollection<StateProvince> StateProvinces { get; set; }
    }
}
