using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AWork.Domain.Models
{
    public partial class ProductVendor
    {
        public int ProductId { get; set; }
        public int BusinessEntityId { get; set; }
        public int AverageLeadTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal StandardPrice { get; set; }
        public decimal? LastReceiptCost { get; set; }
        public DateTime? LastReceiptDate { get; set; }
        public int MinOrderQty { get; set; }
        public int MaxOrderQty { get; set; }
        public int? OnOrderQty { get; set; }
        public string UnitMeasureCode { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Vendor BusinessEntity { get; set; }
        public virtual Product Product { get; set; }
        public virtual UnitMeasure UnitMeasureCodeNavigation { get; set; }
    }
}
