using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Models;
using System;

namespace AWork.Contracts.Dto.Sales.SalesTerritory
{
    public class SalesTerritoryForCreateDto
    {
        public string Name { get; set; }
        public string CountryRegionCode { get; set; }
        public string Group { get; set; }
        public decimal SalesYtd { get; set; }
        public decimal SalesLastYear { get; set; }
        public decimal CostYtd { get; set; }
        public decimal CostLastYear { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
