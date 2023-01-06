using AWork.Contracts.Dto.Sales.SalesTerritory;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;

namespace AWork.Contracts.Dto.PersonModule
{
    public class StateProvinceDto
    {
        public int StateProvinceId { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }
        public bool? IsOnlyStateProvinceFlag { get; set; }
        public string Name { get; set; }
        public int TerritoryId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual CountryRegionDto CountryRegionCodeNavigation { get; set; }
        public virtual SalesTerritoryDto Territory { get; set; }
        public virtual ICollection<AddressDto> Addresses { get; set; }
       
    }
}
