using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.Sales.SalesTerritory;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWork.Contracts.Dto.Sales.SalesPerson
{
    public class SalesPersonForCreateDto
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
    }
}
