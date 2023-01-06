using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.SalesCustomer;
using AWork.Contracts.Dto.Sales.SalesPerson;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;

namespace AWork.Contracts.Dto.Sales.SalesTerritory
{
    public class SalesTerritoryDto
    {
        public int TerritoryId { get; set; }
        public string Name { get; set; }
        public string CountryRegionCode { get; set; }
        public string Group { get; set; }
        public decimal SalesYtd { get; set; }
        public decimal SalesLastYear { get; set; }
        public decimal CostYtd { get; set; }
        public decimal CostLastYear { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual CountryRegionDto CountryRegionCodeNavigation { get; set; }
        public virtual ICollection<StateProvinceDto> StateProvinces { get; set; }
        public virtual ICollection<CustomerDto> Customers { get; set; }
    }
}
