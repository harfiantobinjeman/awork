using System;

namespace AWork.Contracts.Dto.Production
{
    public class ProductInventoryDto
    {
        public int ProductId { get; set; }
        public short LocationId { get; set; }
        public string Shelf { get; set; }
        public byte Bin { get; set; }
        public short Quantity { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public virtual LocationDto Location { get; set; }
        public virtual ProductDto Product { get; set; }
    }
}
