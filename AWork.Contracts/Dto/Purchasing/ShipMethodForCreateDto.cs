using System;

namespace AWork.Contracts.Dto.Purchasing
{
    public class ShipMethodForCreateDto
    {
        public string Name { get; set; }
        public decimal ShipBase { get; set; }
        public decimal ShipRate { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
