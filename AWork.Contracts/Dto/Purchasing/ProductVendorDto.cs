using AWork.Contracts.Dto.Production;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Purchasing
{
    public class ProductVendorDto
    {
        public int ProductId { get; set; }
        public int BusinessEntityId { get; set; }
        public int AverageLeadTime { get; set; }
        public decimal StandardPrice { get; set; }
        public decimal? LastReceiptCost { get; set; }
        public DateTime? LastReceiptDate { get; set; }
        public int MinOrderQty { get; set; }
        public int MaxOrderQty { get; set; }
        public int? OnOrderQty { get; set; }
        public string UnitMeasureCode { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual VendorDto BusinessEntity { get; set; }
        public virtual ProductDto Product { get; set; }
        public virtual UnitMeasure UnitMeasureCodeNavigation { get; set; }
    }
}
