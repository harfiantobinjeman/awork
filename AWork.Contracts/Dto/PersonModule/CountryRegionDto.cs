using AWork.Contracts.Dto.Sales.SalesTerritory;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWork.Contracts.Dto.PersonModule
{
    public class CountryRegionDto
    {
        public string CountryRegionCode { get; set; }

        [Display(Name ="Country Region")]
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<CountryRegionCurrencyDto> CountryRegionCurrencies { get; set; }
        public virtual ICollection<SalesTerritoryDto> SalesTerritories { get; set; }
        public virtual ICollection<StateProvinceDto> StateProvinces { get; set; }
    }
}
