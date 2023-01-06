using AWork.Domain.Models;
using System;
using System.Collections.Generic;

namespace AWork.Contracts.Dto.Production
{
    public class LocationDto
    {
        public short LocationId { get; set; }
        public string Name { get; set; }
        public decimal CostRate { get; set; }
        public decimal Availability { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<ProductInventoryDto> ProductInventories { get; set; }
    }
}
