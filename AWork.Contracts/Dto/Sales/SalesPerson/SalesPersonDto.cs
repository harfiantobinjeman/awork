using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.Sales.SalesTerritory;
using AWork.Contracts.Dto.Sales.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWork.Contracts.Dto.Sales.SalesPerson
{
    public class SalesPersonDto
    {
        [Required]
        [Display(Name = "Bussiness Entity ID")]
        public int BusinessEntityId { get; set; }

        [Display(Name = "Territory ID")]
        public int? TerritoryId { get; set; }

        [Display(Name = "Sales Quota")]
        public decimal? SalesQuota { get; set; }

        [Required]
        [Display(Name = "Bonus")]
        public decimal Bonus { get; set; }

        [Required]
        [Display(Name = "Commission")]
        public decimal CommissionPct { get; set; }

        [Required]
        [Display(Name = "Sales YTD")]
        public decimal SalesYtd { get; set; }

        [Required]
        [Display(Name = "Sales Last Year")]
        public decimal SalesLastYear { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual SalesTerritoryDto Territory { get; set; }
        public virtual EmployeeDto BusinessEntity { get; set; }
        public virtual ICollection<StoreDto> Stores { get; set; }
        public virtual List<StoreDto> StoreDtos { get; set; }
    }
}
